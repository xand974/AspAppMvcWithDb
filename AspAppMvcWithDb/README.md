Création d'un site type blog avec base de données

Setup => ASP .NET Core vide => ASP .NET MVC

Etape 1 : setup dossiers pour MVC 
	-Models
	-Views
	-Controllers
Etape 2 : setup dossier pour design pattern MVVM
	-ViewModels

Le but étant de faire passer des données directement dans les views en passant par une class ViewModel,
car le faire avec le model peut devenir rapidement compliqué.

Etape 3 : setup Model

Création de deux classes Post et Creator pour avoir un lien entre les postes crées et les créateurs de celle-ci

Etape 4:

Setup de la logique pour les opérations CRUD des postes en utilisant une interface => par extension, setup de la dependency injection.
Creation d'une liste fictive avant de créer une base de données
Setup de la dependency injection dans le Startup.cs 
	- utilisation de AddTransient<> : à chaque fois qu'on appelera le service, une nouvelle instance de l'implémentation de l'interface sera crée, si c'est la même requête HTTP ou non
	

Etape 5 : 
Setup du controller qui va gérer les HttpRequests provenant du client
Ajout de l'interface dans le ctor du controller => va appeler son implémentation pour effectuer les op

Etape 6 : Views

ViewStart : Exporter le layout dans toutes les views
ViewImport : namespace qu'on utiliser dans les views (notamment les ViewModels) + TagHelpers
	-Shared 
		-Layout avec bootstrap
	-Home
		-Index : Affiche dynamiquement la liste des postes
		-Detail
		-Create

