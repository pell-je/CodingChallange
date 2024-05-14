# JobTargetCodingChallange

I created a Github repo for this coding challange.
The master branch is the OrderController service as was sent to me via the emailed order_api_cs.txt file.
My solution for this Coding Challange can be seen in this Pull Request https://github.com/pell-je/CodingChallange/pull/1/files


I found four issues with the OrderController:
1. The Id was being set with a Random. This is not a reliable way to generate ids. The random could generate a value that is already used.
2. The return value of Random.Next is an int. The ID value of the Order entity is a string.
3. A semi-colon is needed after the last curly brace instantiate the Order object.
4. The AppDbContext does not exsist.

How my changes fix these issues:
1. I removed setting the Id in the backend. I left the database to use auto-increment/identity to set it.
2. I changed the Id type of the Order entity to a long. This is a much better type for an entity that will likly be stored in a database. A string Id could also be used, especially if its used in a nosql database. I choose to use a long here. The Id database column could be a BIGINT if using a sql database.
3. I added a semi-colon.
4. I created a class for AppDbContext and added it to the dependecny injection container.

Some other changes I made for Quaility of Code. 
1. Use a service layer to access the database. Using a service layer in the controller allows for code reuse and decouples logic.
2. Use swagger/OpenAPI to test endpoints easily.
3. Use the ControllerExceptionFilter to handle exceptions globally
4. Use the required keyword to reduce null pointer exceptions 


Swagger Screenshots:</br>
![image](https://github.com/pell-je/CodingChallange/assets/90728658/9957dd2e-5277-4b20-9550-0d80d76f0b17)

Order POST Request:</br>
![image](https://github.com/pell-je/CodingChallange/assets/90728658/d815443d-1ab0-4f62-abff-f74d10cf5b78)

Order POST Response:</br>
![image](https://github.com/pell-je/CodingChallange/assets/90728658/85fffe6c-802c-4c6c-8298-ef6c215ffcb8)

Order GET Request:</br>
![image](https://github.com/pell-je/CodingChallange/assets/90728658/d83f7e95-0008-473a-b956-eb9c1a4cc2fb)

Order GET Response: </br>
![image](https://github.com/pell-je/CodingChallange/assets/90728658/d99d5f72-cc20-49c9-ab7d-06d9cb9d8c4b)

Order GET Request That should return with Bad Request </br>
![image](https://github.com/pell-je/CodingChallange/assets/90728658/d2bd98f7-d096-48ec-95ab-6b1d548629d3)

Order GET Response  That should return with Bad Request </br>
![image](https://github.com/pell-je/CodingChallange/assets/90728658/7bdce262-c2ac-4152-922a-5fdcfebb9925)

