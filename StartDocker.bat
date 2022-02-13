docker run 
-d 
-p 8080:80 
--mount type=bind,source="$(pwd)/JonDJones.Website/wwwroot",destination=/app/wwwroot 
--mount type=bind,source="$(pwd)/JonDJones.Website/uSync",destination=/app/uSync 
--mount type=bind,source="$(pwd)/Umbraco.Models",destination=/Umbraco.Models 
--mount type=bind,source="$(pwd)/JonDJones.Website/Views",destination=/app/Views 
-v log-volume:/app/umbraco/Logs 
--name umbraco 
umbraco9starterkit 
--env ASPNETCORE_ENVIRONMENT=Docker