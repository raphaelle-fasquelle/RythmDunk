# RythmDunk_MWMTest

Prototype réalisé sur Unity2019.2.21f

Au total, RythmDunk a été développé en environ une journée de travail.

La moitié du temps a été consacrée aux fonctionnalités demandées du gameplay.

Le reste du temps a été consacrée aux visuels, feedbacks et à quelques ajouts tels que les niveaux de difficultés et la prise en charge d'un best score

Avant cela j'ai également passé un peu de temps à étudier les fonctionnalités de gameplay demandées, et à analyser le jeu de référence afin de pouvoir prioriser les différentes tâches.

# Gestion de la difficulté du jeu

Je trouve le jeu assez difficile, c'est pour  cela que j'ai voulu ajouter un choix de difficulté qui diminue le nombre de balles lancées.
Pour cette même raison je n'ai pas mis de collider sur les bords du panier afin de permettre aux joueurs de rattraper une balle "au dernier moment"

# Choix de programmation

- J'ai voulu utiliser de l'Object Pooling pour les balles pour éviter les potentielles chutes de fps sur mobile dûes à des Destroy().
- J'ai préféré développer le jeu en 3D pour donner au joueur une meilleure impression de profondeur.

# Problèmes rencontrés
- Les quatre zones ne sont pas exactements alignées avec les positions de spawns des balles et les positions du fait de la perspective de la caméra (ce problème ne se poserait pas pour un jeu en 2D).
- Sur PC, la musique ne se lit pas toujours de manière fluide. Je n'ai pas pu identifier l'origine du problème, qui par ailleurs n'intervient jamais sur téléphone.

# Pour aller plus loin
- Ajouter une barre de progression permettant au joueur de se situer dans le niveau.
- Ajouter la possibilité de mettre le jeu en pause.
- Ajouter plusieurs tracks (et donc créer un système de gestion des niveaux).
- Ajouter un leaderboard.
- Ajouter des skins de cerceaux et de balles.
- Ajouter la possibilité d'utiliser la musique présente sur le téléphone du joueur.
