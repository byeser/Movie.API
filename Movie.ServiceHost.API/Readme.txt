1-The Repository Pattern was used because it is a design pattern that reduces access and management to data to a single point.

2-

3-I did not use the authentication mechanism, the task is not explained exactly what to do.

4-In order to update the data for 10 minutes, a javascript is written in the ui section and a javascript is called in 10 minutes with the setInterval command. Called javascript starts updating from api.
temporarily done with javascript but can be done by api in another way.

5-If I had more time, a better design could be done on the ui side and verification could be done and Castle Windsor could be added

-The structure in Omdbapi integration can be easily changed.If you want to use another API, you can integrate directly with class and interface.
-Log recording completed.
-Cache completed.
-Swagger completed.
-Docker-Compose.yml  completed

https://localhost:5001/api/Films/GetAll
https://localhost:5001/api/Films/Get/Batman
https://localhost:5001/api/cachemanagement/clear