min			max

0.1			0.8








Raonament: Al classificar documents en clusters, ens interessa capturar els termes rellevants dins el cluster que no apareguin als altres clusters.
Aquests termes que ens permetern definir millor un cluster no poden apareixer en la majoria de documents ja que sinó no serien una característica diferenciadora del cluster.
- Ens interessa considerar termes únics de cluster (minFreq baixa i maxFreq no molt alta). maxFreq no molt alta també és important en la primera iteració, donat que
una maxFreq elevada implica que apareguin termes molt comuns entre els documents i, si aquests són significatius en nombre pot provocar que en la primera iteració, al inicialitzar
els clusters aleatoriament, tots els documents s'assignin a un mateix cluster.
- Al mateix temps tampoc ens interessa considerar termes extremadament poc ocurrents ja que segurament siguin soroll i incrementen el nombre de termes i el temps de càlcul.
Per les raons exposades els experiments es basaran en aquesta sèrie de combinacions:
min		max
0.1     0.8				vocab: 296 words	
0.1		0.6				vocab: 292 words	wordsxtext not changing much, nearly the same
0.1     0.3				vocab: 258 words		wordsxtext are reduced in a reasonable amount
Veurem si realment empitjora amb la baixada de maxFreq
com que amb 0.6 de maxfreq i 0.8 no hi ha diferencia de vocab ni paraules per text fixem 0.6 i 0.3
0.05		0.6			vocab: 635 words	wordsxtext are increased by a huge amount 2x normally
0.2			0.6			vocab: 95  words 		wordsxtext	decreased significantly
0.05		0.3			Not tried since 0.05 gives too many words in vocabulary
0.2			0.3			vocab: 61 words		wordsxtext decreased significantly