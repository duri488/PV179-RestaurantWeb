# PV179-RestaurantWeb
School project for PV179

## Team Members
Juraj Lazorík	527401\
Denis Lebó	527331\
Pavol Bock	506490

## Description
Projekt bude obsahovat jak prezentační web, tak administraci restaurace.

Prezentační web bude mít:
- Popis restaurace
- Týdenní menu na tento týden (dynamicky měněné podle aktuálního týdne)
- Nápojový a jídelní lístek
- Kontakty + kontaktní formulář
- Mapu s vyznačenou lokací provozovny (ne screenshot)
- Minimálně dvě jazykové mutace

Administrace bude mít:
- Přihlášení uživatele, nic jiného než login stránka nebude pro nepřihlášené dostupné
- Správu jídelního a nápojového lístku
- Správu denního menu (i na několik týdnů dopředu), včetně exportu a importu (např. z/do CSV nebo Excelu)
- Správu překladů webu

## How to set up SMTP server for contact form
Contact formular sends email after submiting.
To verify this function localy you need SMTP server.
1. Install PapercutSMTP (https://github.com/ChangemakerStudios/Papercut-SMTP) from root directory (Papercut.Smtp.Setup.exe)
2. Open Papercut application
3. Start Restaurant web
4. Submit contact formular
