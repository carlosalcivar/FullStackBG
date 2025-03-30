using Microsoft.IdentityModel.Tokens;
using ms_loguearusuario.Controllers.Type;
using ms_loguearusuario.Repository.Contract;
using ms_loguearusuario.Repository.Model;
using ms_loguearusuario.Service.Contract;
using ms_loguearusuario.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ms_loguearusuario.Service.Impl
{
    public class ServiceImpl : IService
    {
        private readonly ILogger<ServiceImpl> _logger;
        private IRepository _repository;

        public ServiceImpl(ILogger<ServiceImpl> logger, IRepository repositorio)
        {
            _logger = logger;
            _repository = repositorio;
        }


        #region Método de autenticación
        public async Task<ResponseLoginType> Login(RequestLoginType requestLoginType)
        {
            try
            {
                _logger.LogInformation($"Inicia metodo de Login - SERVICE");

                ResponseLoginType responseLoginType = new ResponseLoginType();
                responseLoginType.Usuario = requestLoginType.Usuario;

                #region Obtener Parametros

                string ISSUER = Environment.GetEnvironmentVariable("ISSUER")!;
                if (string.IsNullOrEmpty(ISSUER)) throw new ServiceException("No esta definida la variable ISSUER") { Codigo = StatusCodes.Status400BadRequest };

                string AUDIENCE = Environment.GetEnvironmentVariable("AUDIENCE")!;
                if (string.IsNullOrEmpty(AUDIENCE)) throw new ServiceException("No esta definida la variable AUDIENCE") { Codigo = StatusCodes.Status400BadRequest };

                string EXPIRE = Environment.GetEnvironmentVariable("EXPIRE")!;
                if (string.IsNullOrEmpty(EXPIRE)) throw new ServiceException("No esta definida la variable EXPIRE") { Codigo = StatusCodes.Status400BadRequest };

                string KEYSECERT = Environment.GetEnvironmentVariable("KEYSECERT")!;
                if (string.IsNullOrEmpty(KEYSECERT)) throw new ServiceException("No esta definida la variable KEYSECERT") { Codigo = StatusCodes.Status400BadRequest };

                string KEYENCRIT = Environment.GetEnvironmentVariable("KEYENCRIT")!;
                if (string.IsNullOrEmpty(KEYENCRIT)) throw new ServiceException("No esta definida la variable KEYENCRIT") { Codigo = StatusCodes.Status400BadRequest };

                #endregion

                #region Validar Autenticacion
                if (!DataValidator.EsBase64Valido(requestLoginType.Password))
                    throw new ServiceException("Usuario o Contraseña incorrectos") { Codigo = StatusCodes.Status400BadRequest };
                //Obtener el string de base 64
                byte[] _password = Convert.FromBase64String(requestLoginType.Password);

                //Consulta usuario
                UsuarioDTO? usuarioDTO = await _repository.ConsultarPorUsuario(requestLoginType.Usuario);

                if (usuarioDTO == null)
                    throw new ServiceException("Usuario o Contraseña incorrestos") { Codigo = StatusCodes.Status400BadRequest };

                if (!Encoding.UTF8.GetString(_password).Equals(CryptoAes.Descifrar(usuarioDTO.Contrasena, KEYENCRIT)))
                    throw new ServiceException("Usuario o Contraseña incorrestos") { Codigo = StatusCodes.Status400BadRequest };

                responseLoginType.Email = usuarioDTO.Email;
                #endregion

                #region Generar Token Autorizador

                var claims = new[]
                {
                    new Claim(ClaimTypes.System, "Tienda"),     // application
                    new Claim(ClaimTypes.Name, usuarioDTO.Usuario),       // Usar nombre como identificador de usuario
                    new Claim(ClaimTypes.Email, usuarioDTO.Email),  // Perfil/rol del usuario
                 };

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(KEYSECERT);//CAMBIAR EL KEY
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(EXPIRE)),//CAMBIAR EL EXPIRE
                    Issuer = ISSUER,//Issuer,Emite  
                    Audience = AUDIENCE,//Audience, Consume
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenskey = tokenHandler.CreateToken(tokenDescriptor);
                responseLoginType.Token = tokenHandler.WriteToken(tokenskey);

                #endregion

                return responseLoginType;
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"Error en metodo de Login - SERVICE {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error metodo de Login - SERVICE {ex.Message}");
                throw;
            }
            finally
            {
                _logger.LogInformation($"Finaliza metodo de Login - SERVICE");
            }
        }
        #endregion
    }
}
