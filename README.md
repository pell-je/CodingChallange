# JobTargetCodingChallange

I created a Github repo for this coding challange.
The master branch includes the OrderController service as was sent to me via the emailed order_api_cs.txt file.
</br></br> 
My solution for this Coding Challange can be seen in this Pull Request:
</br> 
https://github.com/pell-je/CodingChallange/pull/1/files

I will delete this repository when my application for the software positon is closed or when I am requested to do so. 


### I found four issues with the OrderController:
1. The Id was being set with a Random. This is not a reliable way to generate ids. The random could generate a value that is already used.
2. The return value of Random.Next is an int. The ID value of the Order entity is a string.
3. A semi-colon is needed after the last curly brace when instantiating the Order object.
4. The AppDbContext does not exsist.

### How my changes fix these issues:
1. I removed setting the Id in the backend. I left the database to use auto-increment/identity to set it.
2. I changed the Id type of the Order entity to a long. This is a much better type for an entity that will likly be stored in a database. A string Id could also be used, especially if its used in a nosql database. I choose to use a long here. The Id database column could be a BIGINT if using a sql database.
3. I added a semi-colon.
4. I created a class for AppDbContext and added it to the dependency injection container.

### Some other changes I made for Quaility of Code. 
1. Use a service layer to access the database. Using a service layer in the controller allows for code reuse and decouples logic.
2. Use swagger/OpenAPI to test endpoints easily.
3. Use the ControllerExceptionFilter to handle exceptions globally
4. Use the required keyword to reduce null pointer exceptions
5. Validate entities before saving to database
6. Add Test Coverage for OrderContoller and OrderService

### Test Results: </br>
![image](https://github.com/pell-je/CodingChallange/assets/90728658/af29b043-82f8-4840-86e9-801f9558f2a1)
</br>
</br>

### Swagger Screenshots:

Order POST</br>
![image](https://github.com/pell-je/CodingChallange/assets/90728658/d39cdedc-94aa-4be9-87e7-61c101cdd32d)
</br>
</br>

Order GET</br>
![image](https://github.com/pell-je/CodingChallange/assets/90728658/c759d9af-18b9-4be7-a8a2-c170cc44573a)
</br>
</br>

Order POST Zip Validation</br>
![image](https://github.com/pell-je/CodingChallange/assets/90728658/595b8851-3e62-491c-885f-95b016cf8ec7)
</br>
</br>

