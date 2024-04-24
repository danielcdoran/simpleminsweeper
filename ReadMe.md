A game my end in 3 ways
1. Player wins ie. they have a series of moves to reach row 8
2. Player looses - player hss hit all the mines
3. Player neither wins or looses - game is abandoned

The board is initialised by placing mines randomly. Since a player may hit multiple mines , the number of mines on the board must
be greater then this otherwise the player ALLWAYS wins. The probability that the cellselected will have a mine inserted is determined by the "fill factor". This must be greater than 0.0 and less that 1.0. Snce this function uses a random number generator, there is no guarantee that s simple iteration through all cells will 
create a sufficient number of mines. Therefore the mine creation cycle is repeated until  the number of mine created is sufficent.
