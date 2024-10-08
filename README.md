# Jumper oefening
## Inleiding
Het doel van de Jumper is om een ml-agent te trainen die over een obstakel kan springen. De bedoeling is dat er ook nog een andere uitdaging wordt toegevoegd. In dit geval is dat een beloning die bonus punten opleverd.
## Overzicht
In deze tutorial worden de methoden en resultaten besproken.
Bij de  methoden wordt besproken wat je moet installeren, wat de jumper doet en krijg je informatie over de observaties, acties en beloningen.
## Methoden
### Installatie
voor het project te laten werken moet je het volgende geinstalleerd hebben:
* Unity versie: 22.3.* 
* Anaconda (conda): 24.1.2
    * Python versie: 3.9.* 
    * ML-Agents Toolkit versie: 0.30.0 
    * PyTorch versie: 1.7.1
### Reproduceren
Je kan het project downloaden en opstarten met:

git clone <https://github.com/AP-IT-GH/jumper-assignment-inge129116.git>

De agent trainen kan in de scene testscene.

### Verloop simulatie
Er is een muur waaruit blokken vertrekken.
Deze blokken gaan richting de Agent. De blokken kunnen hindernissen of beloningen zijn.
De agent moet over de hindernissen springen.
De beloningen mag hij wel raken.
Het verschil kan hij zien via de tags van de objecten.
### Overzicht observaties, mogelijke acties en beloningen
#### Observaties
* eigen positie
* rayperceptionSensor3D (met tags)

![observations](https://github.com/AP-IT-GH/jumper-assignment-inge129116/blob/a3755de8e78d85abc7dab210ef3aa477d4185a81/image_observations.png)

![agent met rays](https://github.com/AP-IT-GH/jumper-assignment-inge129116/blob/main/image_agent_rays.png)

#### Mogelijke acties en beloningen
De agent heeft 2 mogelijke acties. Hij kan springen of niet springen. Dit werkt met discrete actions.

![jump code](https://github.com/AP-IT-GH/jumper-assignment-inge129116/blob/a3755de8e78d85abc7dab210ef3aa477d4185a81/image_jump_discrete.png)

Hij wordt beloond als:
* een hindernis niet geraakt wordt
* een beloning wel geraak wordt

Hij wordt gestraft als:
* de agent springt (dit is een kleine straf zodat hij niet constant springt.)
* een hindernis geraakt wordt.
### Beschrijvingen objecten
* hindernis: obstakel waar de agent over moet springen
* beloning: kubus die de agent moet raken.
* Agent: een ML-Agent die getraind wordt om over hindernissen te springen.

## Resultaten
### Grafieken

![grafieken](https://github.com/AP-IT-GH/jumper-assignment-inge129116/blob/3fc35c366131040b947ae323281422999ee23105/grafieken.png)
### Cumulative Reward
Deze stijgt over het algemeen, dit betekent dat de agent beter wordt.
### Episode Length
De episode eindigt wanneer een hindernis geraakt wordt, dus hoe langer de lengte van een episode hoe beter de agent bezig is.
### Entropy
Dit is hoe onzeker de agent is. Deze waarde daalt heel snel. Dit komt overeen met de snel stijgende waarde in het begin bij de Cumulative Reward. Bij een lage waarde voor entropy is de agent zeker van wat hij doet.
### Bespreking resultaat
Het model leert in het begin heel snel. 
Daarna wordt het model zekerder en verandert het steeds minder. 
De waarde van de beloningen lijken nog niet perfect maar met deze training kan het model al goede resultaten halen. Hij springt over de meeste hindernissen en springt niet constant waardoor hij kan zien of er iets aankomt. Het model springt ook niet over de beloningen.
