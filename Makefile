
rabbit :
	docker run -d --hostname my-rabbit --name some-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management

dockerup:
	docker-compose up -d --force-recreate && docker-compose build --force-rm
	
entity:
	dotnet ef database update --project Infrastructure --startup-project Webshop --context WebshopDbContext
	
project: rabbit dockerup entity