TDU MODDING TOOLS 1.13
----------------------

English Readers, please scroll down.

Nouveautés
    - Général :
		* Correction de bugs divers.
    - File Browser :
		* Quand le fichier à extraire existe déjà, le précédent est renommé en *_old
		* Nouveau paramètre permettant d'afficher ou non les fichiers dans l'explorateur après extraction; la question n'est plus posée
		* EditTasks : Suppression du bouton 'Settings', devenu inutile
		* EditTasks : Permet d'afficher les propriétés d'un fichier en cours d'édition
		* Le fichier cameras.bin a maintenant sa propre icône et possède des propriétés consultables.
	- Graphics Converters :
		* Quand le fichier converti existe déjà, celui-ci est renommé en *_old
		* DDS>2DB : option 'Keep original file size' obsolète donc supprimée
		* DDS>2DB : nouvelle option pour changer le nom interne de la texture.
	- Patch Editor :
		* Changements graphiques mineurs
		* Changement du raccourci clavier pour déplacement d'instruction (Ctrl-Haut, Ctrl-Bas)
		* Taille initiale de la fenêtre 1024x768
		* Boîte d'aide sur l'instruction en cours
		* Possibilité d'écrire un commmentaire par instruction
		* Nouveau système de rôle-auteur pour gérer les crédits
		* Nouvelle instruction : setTrafficDistribution
		* Import/export d'instruction via un fichier XML (export simple pour le moment). Pour importer les fichiers depuis TDUPE, veuillez utiliser la dernière mise à jour de Dredgy.
	- Vehicle Manager :
		* Datasheet : données de performance ajoutées (seules celles compatibles en ligne sont modifiables)
		* Physics > Gearbox : réglage rapide de la longueur des rapports (-50% à +50%)
		* Colors : gestion des couleurs d'intérieur (ordre d'affichage - codes couleur - prix ...).
	- ModAndPlay (installeur) :
		* Changement de disposition des éléments
		* Affiche les rôles et auteurs tels que renseignés dans l'éditeur de patchs.
	- Modding Library (pour les développeurs):
		* Nouvelle méthode GetEntriesFromValue(...) dans la classe DBResource, retournant toutes les entrées ayant la valeur demandée (incluant des options de recherche).

Crédits :
	- Conception et programmation: 2CVSUPERGT / Djey
	- Tests: 2CVSUPERGT, cjr70, jcmmota, kennybzh, philippeapo, reilsss, tool831...


Merci à tous.


Djey.


-------
ENGLISH
-------

Changes
    - Main:
		* Misc bugs fixed.
    - File Browser:
		* When extracted file already exists, previous one is renamed to *_old
		* New setting allowing to show/hide explorer window after extract; question is not asked anymore
		* EditTasks: useless 'Settings' button removed
		* EditTasks: allow to watch properties of a currently edited file
		* 'cameras.bin' file gets its own icon now and reveals a few properties to consult.
	- Graphics Converters:
		* When converted file already exists, previous one is renamed to *_old
		* DDS>2DB: obsolete 'Keep original file size' option removed
		* DDS>2DB: added new option to change texture's internal name.
	- Patch Editor:
		* Minor visual fixes
		* Keyboard shortcut changed for instruction moving (Ctrl-up, Ctrl-down)
		* Window has initial size now 1024x768
		* New help dialog box prviding info about current instruction
		* Enables to write a comment per instruction
		* New role-author system to properly manage credits
		* New instruction: setTrafficDistribution
		* Instruction Importer/exporter via XML file (export is rather simple for now). To import from TDUPE, make sure you're using latest Dredgy's update.
	- Vehicle Manager:
		* Datasheet: added performance data (only online-safe ones can be modified)
		* Physics > Gearbox : quick (and dirty) gears length setting (-50% to +50%)
		* Colors: interior colors now handled (display order - color codes and materials - price ...).
	- ModAndPlay (installer):
		* Item layout changed
		* Displays roles and authors as defined in PatchEditor.
	- Modding Library (for developers):
		* New method GetEntriesFromValue(...) in DBResource, returning all entries with specified value (matching options included).

Credits:
	- Design and programming: 2CVSUPERGT / Djey
	- Testing: 2CVSUPERGT, cjr70, jcmmota, kennybzh, philippopo, reilsss, tool831...


Thanks to people making all those things real.


Djey.