dev/up:
	docker compose -f ./deploy/docker-compose.yml up -d mongodb
	
dev/down:
	docker compose -f ./deploy/docker-compose.yml down
