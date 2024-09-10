# Ce que fait ce `README.md` :

- **Caract�ristiques** : D�crit ce que fait le microservice `ms-recip`.
- **Pr�requis** : �num�re les outils n�cessaires pour construire et ex�cuter le service.
- **Installation** : Fournit des instructions pour cloner le d�p�t et acc�der au r�pertoire.
- **Configuration** : Explique que le microservice utilise une base de donn�es SQLite.
- **Construire et Ex�cuter le Microservice** : Explique comment construire et ex�cuter le service localement et via Docker.
- **Utilisation** : Explique comment utiliser les points de terminaison OData pour interagir avec le service.
- **Exemples de Requ�tes avec Postman** : Fournit des exemples concrets de requ�tes pour tester le service.
- **Contribuer** : Encouragement � contribuer au projet.
- **License** : D�crit la licence sous laquelle le projet est distribu�.

## Caract�ristiques
`ms-crecip` est un microservice bas� sur .NET Minimal APIs et OData qui sert de point central pour g�rer les recettes dans une architecture orient�e �v�nements (Event-Driven Architecture). Ce microservice permet de g�rer et de r�cup�rer les recettes � travers une API REST OData.

- **OData API** : Fournit une API RESTful utilisant OData pour g�rer les recettes.
- **Minimal APIs** : Utilise le mod�le l�ger de Minimal APIs de .NET pour une configuration plus simple.
- **SQlite Database** : Utilise une base de donn�es SQLite pour stocker les recettes.
- **Containerisation** : Conteneuris� � l'aide de Docker pour un d�ploiement facile.

## Pr�requis

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)

## Installation

Clonez ce d�p�t et acc�dez au r�pertoire du projet :

```bash
git clone https://github.com/brendanGiraudet/ms-recip.git
cd ms-recip
```

## Configuration
Il n'y a pas de configuration n�cessaire pour le d�veloppement, car le microservice utilise la migration de base de donn�e.

Construire et Ex�cuter le Microservice

1. Construire le Projet
Assurez-vous que vous avez le .NET 8 SDK install�, puis construisez le projet :

```bash
Copier le code
dotnet build
```

2. Ex�cuter le Projet Localement
Ex�cutez le microservice localement pour le tester :

```bash
Copier le code
dotnet run
```

Le microservice sera disponible � l'adresse http://localhost:5000 (port specifi� dans le fichier appsettings).

3. Utiliser Docker
Construire l'Image Docker et/ou l'�x�cuter
Pour construire l'image Docker du microservice, utilisez la commande suivante :

```bash
Copier le code
docker compose -f .\docker-compose-debug.yml up
```

Le service sera disponible � http://localhost:1919 (port specifi� dans le fichier docker-compose-debug).

## Utilisation
Points de Terminaison OData
Le microservice expose plusieurs points de terminaison OData pour g�rer les recettes.

GET /odata/Recips : R�cup�re toutes les recettes.
GET /odata/Recips({key}) : R�cup�re une recette sp�cifique par ID.
POST /odata/Recips : Ajoute une nouvelle recette.
PUT /odata/Recips({key}) : Met � jour une recette existante.
DELETE /odata/Recips({key}) : Supprime une recette sp�cifique.

## Exemples de Requ�tes avec Postman
R�cup�rer toutes les recettes
M�thode : GET
URL : http://localhost:8080/odata/Recpis

Ajouter une nouvelle configuration RabbitMQ
M�thode : POST
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
Les contributions sont les bienvenues ! Veuillez soumettre une issue ou une pull request pour toute am�lioration ou probl�me.

## License
Aucune licence

