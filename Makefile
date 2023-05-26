
dockerup:
	docker compose up
	
dockerbuild:
	docker compose build
	
project: dockerbuild dockerup