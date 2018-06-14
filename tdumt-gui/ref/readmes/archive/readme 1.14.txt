TDU MODDING TOOLS 1.14
----------------------

English Readers, please scroll down.

Nouveautés
    - Général :
		* Correction de bugs divers
			+ Bug de remplacement dans certains BNK
			+ Détection de la version et du dossier d'install de TDU sur les systèmes 64 bits
			+ Correction de conflit entre les instructions InstallFile et InstallPackedFile
		* Suppression du MAP Tool (maintenant inutile).
    - File Browser :
		* Les opérations suivantes gèrent maintenant une sélection multiple: Property, Backup 
	- Patch Editor :
		* Apparence de la boîte d'aide modifiée
		* Possibilité de saisir une remarque libre pour le patch
		* Export multiple d'instructions supporté
		* Les instructions d'installation peuvent être groupées :
			+ au moins un groupe requis : 'Required' (ce nom peut être personnalisé)
			+ un groupe d'instructions peut dépendre d'un autre
			+ des groupes peuvent s'exclure mutuellement
		* Nouvelle variable: bnkVehicleTrafficPath, pointant vers Vehicules\Traffic
		* Nouvelle variable: bnkFrontEndHiResPath, pointant vers FrontEnd\HiRes
		* Nouvelle instruction: customizeViews, permettant d'utiliser sur la caméra les vues d'une autre caméra
		* Nouvelle instruction: setTrafficPerKilometer, permettant de définir la densité du traffic par zone de jeu.
	- Vehicle Manager :
		* Cam-IK
			+ possibilité d'utiliser une caméra propre à chaque slot du jeu
			+ customisation de chaque vue en utilisant la vue d'une caméra existante (veuillez rester coéhrent dans le type de vue !)
		* Datasheet
			+ changements de présentation des marques dans le sélecteur
			+ si disponible, le logo de la marque est maintenant apposé à côté du nom (+ de 370 logos de marques disponibles)
			+ modification du limiteur de vitesse (baisse seulement autorisée)
		* Physics:
			+ support du régime moteur au démarrage
			+ modification des temps de démarrage, montée/descente en régime
			+ modification du type de carrosserie (berline, coupé, cabriolet...)
			+ gestion des dimensions (Lxlxh)
		* Colors: copie possible des sets d'intérieur depuis un extérieur vers un autre.
	- ModAndPlay (installeur) :
		* Si le chemin de TDU est modifié, l'état du mod (installé/désinstallé) est bien mis à jour (+ mise à jour du bouton 'Uninstall')
		* Affichage de la remarque libre sous le titre du mod
		* Suppression du paramétrage de la langue, car non utilisé
		* Si plusieurs groupes d'instructions sont présents, donne la possibilité de sélectionner lesquels installer.
	- Modding Library (pour les développeurs):
		* Gestion du fichier AIConfig.xml
		* Merci de bien renseigner la property Tools.TduPath au démarrage de votre application.

Crédits :
	- Conception et programmation: 2CVSUPERGT / Djey
	- Tests: 2CVSUPERGT, Djey, Dredgy, reilsss, Tool831, ...
	- Logos: 2CVSUPERGT, christophe31fr, xiorxorn


Merci à tous.


Djey.


-------
ENGLISH
-------

Changes
    - Main:
		* Misc bug fixed
			+ Replacing bug in some BNKs, corrupting them
			+ TDU version and install folder detection on 64 bit systems
			+ Solves a conflict between InstallFile and InstallPackedFile instructions
		* MAP Tool removed (now useless).
    - File Browser:
		* Following operations now apply to many selected files: Property, Backup 
	- Patch Editor:
		* Changed help box appearance
		* Ability to enter a free remark for the patch
		* Multiple instruction export hnow handled
		* Install instructions can be put in groups:
			+ at least one required group : 'Required' (this name can be customized)
			+ an instruction group can rely on another
			+ groups can exclude mutually
		* New variable: bnkVehicleTrafficPath, heading to Vehicules\Traffic
		* New variable: bnkFrontEndHiResPath, heading to FrontEnd\HiRes
		* New instruction: customizeViews, allowing to use views from a genuine camera to another
		* New instruction: setTrafficPerKilometer, allowing to set traffic density per TDU map zone.
	- Vehicle Manager:
		* Cam-IK
			+ ability to use a unique camera per vehicle slot
			+ view customization by using view from existing camera (please do not mess view types though !)
		* Datasheet
			+ changes in brand layout in selector
			+ if available, brand logo is displayed next to its name (370+ brand logos available)
			+ ability to set speed limiter (lowering is authorized only to keep compatibility in online mode)
		* Physics:
			+ starting engine rpm support
			+ starting, rev-up/down times can be modified
			+ bodywork type can be changed (sedans, coupé, roadster...)
			+ body dimensions now handled (lxwxh)
		* Colors: ability to copy interior sets from an exterior model to another.
	- ModAndPlay (installer) :
		* If TDU install path has been changed, mod status (installed/uninstalled) is updated (+ 'Uninstall' button reflected)
		* Free patch remark is now displayed under mod title
		* Useless language setting now removed as useless with current instructions
		* If many intruction groups have been set, a new box now appears, giving the ability to select which ones to install.
	- Modding Library (for developers):
		* AIConfig.xml file handled
		* Please ensure to put right value to Tools.TduPath property when your app starts.

Credits:
	- Design and coding: 2CVSUPERGT / Djey
	- Tests: 2CVSUPERGT, Djey, Dredgy, reilsss, Tool831, ...
	- Logos: 2CVSUPERGT, christophe31fr, xiorxorn


Thanks to all who make those things real.


Djey.