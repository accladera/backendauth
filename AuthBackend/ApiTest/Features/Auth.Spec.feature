Feature: Probar las funcionalidades de auth



Scenario: Creacion de un usuario cliente 
    Given la siguiente solicitud
    """
    {
      "email": "gonzalo@gmail.com",
      "name": "gonzalo",
      "password": "12345",
      "birthDate": "2021-12-04T03:14:20.797Z"
    }
    """
    When se solicita "sin" credenciales que se procese a la url "/api/auth/user/store/client", usando el metodo "post"
    Then la respuesta debe tener el codigo de estado 200 
    Then imprimir la respuesta


Scenario: Creacion de un cliente incorrecto
    Given la siguiente solicitud
    """
    {
      "email": "asdasdasd",
      "name": "gonzalo",
      "password": "12345",
      "birthDate": "2021-12-04T03:14:20.797Z"
    }
    """
    When se solicita "sin" credenciales que se procese a la url "/api/auth/user/store/client", usando el metodo "post"
    Then la respuesta debe tener el codigo de estado 400 
    And la respuesta debe contener un error validacion con estos campos definidos
    """
        "El email no cumple con el formato correcto"
    """

    
Scenario: Creacion de un cliente seed
    Given la siguiente entidad "authclient" registrada
    """
    {
      "email": "gonzalo@gmail.com",
      "name": "gonzalo",
      "password": "12345",
      "birthDate": "2021-12-04T03:14:20.797Z"
    }
    """
    