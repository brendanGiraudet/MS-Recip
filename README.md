# Ce que fait ce `README.md` :

- **Caractéristiques** : Décrit ce que fait le microservice `ms-recip`.
- **Prérequis** : Énumère les outils nécessaires pour construire et exécuter le service.
- **Installation** : Fournit des instructions pour cloner le dépôt et accéder au répertoire.
- **Configuration** : Explique que le microservice utilise une base de données SQLite.
- **Construire et Exécuter le Microservice** : Explique comment construire et exécuter le service localement et via Docker.
- **Utilisation** : Explique comment utiliser les points de terminaison OData pour interagir avec le service.
- **Exemples de Requêtes avec Postman** : Fournit des exemples concrets de requêtes pour tester le service.
- **Contribuer** : Encouragement à contribuer au projet.
- **License** : Décrit la licence sous laquelle le projet est distribué.

## Caractéristiques
`ms-crecip` est un microservice basé sur .NET Minimal APIs et OData qui sert de point central pour gérer les recettes dans une architecture orientée événements (Event-Driven Architecture). Ce microservice permet de gérer et de récupérer les recettes à travers une API REST OData.

- **OData API** : Fournit une API RESTful utilisant OData pour gérer les recettes.
- **Minimal APIs** : Utilise le modèle léger de Minimal APIs de .NET pour une configuration plus simple.
- **SQlite Database** : Utilise une base de données SQLite pour stocker les recettes.
- **Containerisation** : Conteneurisé à l'aide de Docker pour un déploiement facile.

## Prérequis

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)

## Installation

Clonez ce dépôt et accédez au répertoire du projet :

```bash
git clone https://github.com/brendanGiraudet/ms-recip.git
cd ms-recip
```

## Configuration
Il n'y a pas de configuration nécessaire pour le développement, car le microservice utilise la migration de base de donnée.

Construire et Exécuter le Microservice

1. Construire le Projet
Assurez-vous que vous avez le .NET 8 SDK installé, puis construisez le projet :

```bash
Copier le code
dotnet build
```

2. Exécuter le Projet Localement
Exécutez le microservice localement pour le tester :

```bash
Copier le code
dotnet run
```

Le microservice sera disponible à l'adresse http://localhost:5000 (port specifié dans le fichier appsettings).

3. Utiliser Docker
Construire l'Image Docker et/ou l'éxécuter
Pour construire l'image Docker du microservice, utilisez la commande suivante :

```bash
Copier le code
docker compose -f .\docker-compose-debug.yml up
```

Le service sera disponible à http://localhost:1919 (port specifié dans le fichier docker-compose-debug).

## Utilisation
Points de Terminaison OData
Le microservice expose plusieurs points de terminaison OData pour gérer les recettes.

GET /odata/Recips : Récupère toutes les recettes.
GET /odata/Recips({key}) : Récupère une recette spécifique par ID.
POST /odata/Recips : Ajoute une nouvelle recette.
PUT /odata/Recips({key}) : Met à jour une recette existante.
DELETE /odata/Recips({key}) : Supprime une recette spécifique.

## Exemples de Requêtes avec Postman
Récupérer toutes les recettes
Méthode : GET
URL : http://localhost:8080/odata/Recpis

Ajouter une nouvelle configuration RabbitMQ
Méthode : POST
URL : http://localhost:8080/odata/Recips
Corps (JSON) :
json
Copier le code
{
  "Id": 2,
  "Hostname": "localhost",
  "Port": 5672,
  "Username": "newuser",
  "Password": "newpassword",
  "Exchange": "new-exchange",
  "RoutingKeys": {
    "create-recip": "ms-recette-queue",
    "create-recip-result": "ms-log-queue"
  }
}

## Contribuer
Les contributions sont les bienvenues ! Veuillez soumettre une issue ou une pull request pour toute amélioration ou problème.

## License
Aucune licence

