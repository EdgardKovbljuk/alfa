# alpha

Tento projekt se zabývá generováním a správou školních rozvrhů.

Pro spuštění tohoto projektu budete potřebovat:

.NET Core 8.1
Visual Studio 2019 nebo novější

Instalace:
git clone https://github.com/EdgardKovbljuk/alfa.git

Architektura

Hlavní Komponenty

Rozvrh.cs
Toto je centrální třída pro správu rozvrhů. Uchovává informace o časech hodin, předmětech a dalších souvisejících datech.

Ucitel.cs a Predmet.cs
Tyto třídy reprezentují učitele a předměty. Jsou propojeny s třídou Rozvrh, umožňují přiřazování učitelů k předmětům a naopak.

Ucebna.cs
Uchovává informace o učebnách, jejich dostupnosti a kapacitě, které jsou důležité pro plánování rozvrhu.

RozvrhGenerator.cs
Zabývá se generováním rozvrhů na základě dostupných dat a kritérií. Spolupracuje s Rozvrh.cs a dalšími třídami pro vytváření optimalizovaných rozvrhů.

Pomocné Komponenty

CsvReader.cs
Třída pro čtení dat z CSV souborů. Je klíčová pro import dat, která jsou potřebná pro generování rozvrhů.

RozvrhUtils.cs
Poskytuje pomocné metody a nástroje pro manipulaci s daty rozvrhů a jejich správu.

Watchdog.cs
Sleduje a zaznamenává aktivity v systému pro účely auditu a bezpečnosti.

Hodnocení a Analýza

RozvrhHodnotitel.cs a Ho dnotitel.cs
Tyto třídy slouží k hodnocení a analýze vygenerovaných rozvrhů. Umožňují posoudit kvalitu a efektivitu rozvrhů na základě různých metrik.

Datové Soubory

rozvrh.csv a nejlepsiRozvrhy.txt
Tyto soubory obsahují data pro generování a hodnocení rozvrhů. CSV soubor slouží jako zdroj dat, zatímco textový soubor může obsahovat záznamy o nejlepších rozvrzích.

Hlavní Vstupní Bod

Program.cs
Tento soubor je hlavním vstupním bodem aplikace. Koordinuje interakce mezi různými komponentami a spouští celý proces generování a správy rozvrhů.

Čtení Dat z CSV Souborů
Pro načtení dat rozvrhu z CSV souboru:
CsvReader reader = new CsvReader("cesta_k_souboru/rozvrh.csv");
var data = reader.NactiData();

Generování Rozvrhu
Pro generování nového rozvrhu:
RozvrhGenerator generator = new RozvrhGenerator(data);
Rozvrh novyRozvrh = generator.VytvorRozvrh();

Záznam a Monitoring Aktivity (Watchdog)
Pro monitorování a záznam aktivity v systému:
Watchdog watchdog = new Watchdog();
watchdog.ZacniMonitorovat();



Autoři
Edgard Kovbljuk
