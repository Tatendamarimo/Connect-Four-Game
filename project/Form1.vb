Public Class Form1

    Private Sub NewGameButton_Click(sender As Object, e As EventArgs) Handles btnNewGame.Click
        Dim result As DialogResult = MessageBox.Show("Press Ok to continue", "Confirm New Game ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
        If result = DialogResult.OK Then Form2.Show() 'load to Form 2
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then Application.Exit()
    End Sub

    Private Sub btnHwToPlay_Click(sender As Object, e As EventArgs) Handles btnHwToPlay.Click
        MessageBox.Show("
To play the game using the textbox:

1. Enter the column number (0-6) in the textbox where you want to place your game piece.
2.  Click the Enter Column button to place your piece in the lowest empty row in the selected column.
3. The game will automatically switch to the other player.
4. Continue taking turns entering column numbers and placing game pieces until one player gets four in a row horizontally, vertically, or diagonally, or the board is full without a winner.
5. If a winner is determined, the game will display a message and prevent further moves.


To play the game using buttons:

1. Click on the button in the column where you want to place your game piece.
2. The piece will be placed in the lowest empty row in the selected column.
3. The game will automatically switch to the other player.
4. Continue taking turns clicking buttons and placing game pieces until one player gets four in a row horizontally, vertically, or diagonally, or the board is full without a winner.
5. If a winner is determined, the game will display a message and prevent further moves.

  To start a new game, click the Start New Game button.", "How to Play", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class


