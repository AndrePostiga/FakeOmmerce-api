include .env

.PHONY: up

up:
	docker-compose build
	docker-compose -f docker-compose.yml up -d
	docker exec db mongoimport --host ${DB_HOST} --username ${DB_USER} --password ${DB_PASS} --authenticationDatabase admin -d ${DB_NAME} -c products --type json --file /seed.json --jsonArray

.PHONY: down

down:
	docker-compose down

.PHONY: logs

logs:
	docker-compose logs -f

.PHONY: rst

rst: down up logs