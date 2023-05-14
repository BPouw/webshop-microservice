
dockerup:
	docker-compose up -d --force-recreate && docker-compose build --force-rm
	
entity:
	dotnet ef database update --project Infrastructure --startup-project Webshop --context WebshopDbContext
	
project: dockerup entity