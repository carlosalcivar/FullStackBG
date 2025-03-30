# Titulo

ADR 1 - Enfoque de diseño de arquitectura

## Status

Propuesto

## Context

Esquema de arquitectura para la elaboración de un sitio web de inventario

## Decision

Se decide por la utilización de el enfoque de diseño "Arquitectura Hexagonal"

## Consequences

Este el enfoque de arquitectura hexagonal logra un acoplamiento flexible con el diseño basado en dominios(DDD)
La escritura de pruebas unitarias resulta más sencillo debido al acoplamiento flexible entre componentes
Existe un nivel alto de complejidad para separar la lógica empresarial del código de infraestructura