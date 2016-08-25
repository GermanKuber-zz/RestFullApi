# RestFullApi
Se construye una api rest , segura , capas de ser utilizado por múltiples clientes.

#Paso 5 - Se implementa Patch

##Modelo de Request
```json
{
  "id": 125,
  "model":  [
      {
        "value": "Nuevo Nombre",
        "path": "/name",
        "op": "replace",
        "from": "string"
      }
    ]  
}```
