using System.Security.Cryptography;
using System.Text;

namespace ms_loguearusuario.Utils
{
    public class CryptoAes
    {
        /// <summary>
        /// Método encripta una cadena de string
        /// </summary>
        /// <param name="texto">Texto a encriptar</param>
        /// <param name="clave">Clave de 32 bytes (256 bits)</param>
        /// <returns>string encriptado</returns>
        public static string Cifrar(string texto, string clave)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(clave);
                aesAlg.GenerateIV(); // Generar un IV aleatorio para cada operación de cifrado

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(texto);
                        }
                    }

                    // Combina el IV aleatorio con los datos cifrados en una cadena
                    byte[] ivBytes = aesAlg.IV;
                    byte[] ciphertextBytes = msEncrypt.ToArray();
                    byte[] combinedBytes = new byte[ivBytes.Length + ciphertextBytes.Length];
                    Buffer.BlockCopy(ivBytes, 0, combinedBytes, 0, ivBytes.Length);
                    Buffer.BlockCopy(ciphertextBytes, 0, combinedBytes, ivBytes.Length, ciphertextBytes.Length);

                    return Convert.ToBase64String(combinedBytes);
                }
            }
        }

        /// <summary>
        /// Método desencripta una cadena de string previamenete encriptada
        /// </summary>
        /// <param name="textoCifrado">Texto encriptado a desencriptar</param>
        /// <param name="clave">Clave de 32 bytes (256 bits) utilizada cuando se encripto el texto</param>
        /// <returns>string desencriptado</returns>
        public static string Descifrar(string textoCifrado, string clave)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(clave);

                // Extraer el IV de los datos cifrados
                byte[] combinedBytes = Convert.FromBase64String(textoCifrado);
                byte[] ivBytes = new byte[aesAlg.BlockSize / 8];
                byte[] ciphertextBytes = new byte[combinedBytes.Length - ivBytes.Length];
                Buffer.BlockCopy(combinedBytes, 0, ivBytes, 0, ivBytes.Length);
                Buffer.BlockCopy(combinedBytes, ivBytes.Length, ciphertextBytes, 0, ciphertextBytes.Length);

                aesAlg.IV = ivBytes;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(ciphertextBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

        }
    }
}
