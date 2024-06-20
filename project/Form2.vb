Imports System.IO

Public Class Form2
    Dim gameboard(6, 6) As Button ' the 7x7 grid of buttons
    Dim player As Integer = 1 ' the current player (1 or 2)
    Dim winner As Boolean = False ' whether there is a winner

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Form1.Hide() ' Hides form 1 when form 2 is displayed
        For i As Integer = 0 To 6
            For j As Integer = 0 To 6
                gameboard(i, j) = CType(GameboardLayout.GetControlFromPosition(j, i), Button)
                gameboard(i, j).Tag = j ' store the column index in the Tag property
                AddHandler gameboard(i, j).Click, AddressOf Btn00_Click  ' attach the click event handler
            Next
        Next
        LabelPlayer.Text = "Player 1's turn"
    End Sub

    Private Sub Btn00_Click(sender As Object, e As EventArgs)
        If winner Then Return ' exit if there is already a winner
        Dim button As Button = DirectCast(sender, Button)
        Dim col As Integer = CInt(button.Tag) ' get the column index from the Tag property
        Dim row As Integer = -1 ' initialize to an invalid value

        ' Find the first empty cell in the clicked column
        For i As Integer = 6 To 0 Step -1
            If gameboard(i, col).Text = "" Then
                row = i

                If row < 0 Then Return

                ' Place the player's piece on the board
                gameboard(row, col).Text = player.ToString()
                If player = 1 Then
                    gameboard(i, col).BackColor = Color.Orange
                    LabelPlayer.Text = "Player 2's turn"
                Else
                    gameboard(i, col).BackColor = Color.GreenYellow
                    LabelPlayer.Text = "Player 1's turn"
                End If

                ' Check for a winner

                CheckForWinner(row, col)
                ' Switch to the other player
                player = If(player = 1, 2, 1)
                Exit For
            End If
        Next

    End Sub
    Private Sub BtnEnterColumn_Click(sender As Object, e As EventArgs) Handles BtnEnterColumn.Click


        If winner Then Return ' game is already won

        Dim column As Integer
        If Integer.TryParse(ColumnTxtbox.Text, column) AndAlso column >= 0 AndAlso column <= 6 Then
            ' Find the first empty cell in the column and place the player's piece there

        ElseIf Not Integer.TryParse(ColumnTxtbox.Text, column) OrElse column < 0 OrElse column > 6 Then
            MessageBox.Show("Please enter a valid column number (0-6).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Rest of the code for placing the piece in the selected column and checking for a winner
        For i As Integer = 6 To 0 Step -1
            If gameboard(i, column).Text = "" Then
                gameboard(i, column).Text = player
                gameboard(i, column).BackColor = If(player = 1, Color.Orange, Color.GreenYellow) ' set button color based on player
                CheckForWinner(i, column) ' check for a winner after each move
                Exit For
            End If
            If i = 0 Then
                MessageBox.Show("Column is full. Please choose another column.")
                Return ' column is full, ignore click
            End If
        Next

        If winner Then
            LabelPlayer.Text = "Winner: Player " & player ' update label to indicate winner
            ResetGame()
        Else
            player = 3 - player ' switch to the other player (1 -> 2, 2 -> 1)
            LabelPlayer.Text = "Turn: Player " & player ' update label to indicate whose turn it is
        End If

    End Sub


    Private Sub CheckForWinner(i As Integer, j As Integer)

        ' Check horizontal
        Dim count As Integer = 0
        For k As Integer = 0 To 6
            If gameboard(i, k).Text = gameboard(i, j).Text Then
                count += 1
            Else
                count = 0
            End If
            If count >= 4 Then
                winner = True
                MessageBox.Show("Player " & player & " wins!")
                ResetGame()
                Return
            End If
        Next

        ' Check vertical
        count = 0
        For k As Integer = 0 To 6
            If gameboard(k, j).Text = gameboard(i, j).Text Then
                count += 1
            Else
                count = 0
            End If
            If count >= 4 Then
                winner = True
                MessageBox.Show("Player " & player & " wins!")
                ResetGame()
                Return
            End If
        Next

        ' Check diagonal (bottom-left to top-right)
        count = 0
        For k As Integer = 0 To 6
            If i - k < 0 Or j - k < 0 Then Exit For
            If gameboard(i - k, j - k).Text = gameboard(i, j).Text Then
                count += 1
            Else
                count = 0
            End If
            If count >= 4 Then
                winner = True
                MessageBox.Show("Player " & player & " wins!")
                ResetGame()
                Return
            End If
        Next

        ' Check diagonal (bottom-right to top-left)
        For k As Integer = 1 To 6
            If i + k > 6 Or j + k > 6 Then Exit For
            If gameboard(i + k, j + k).Text = gameboard(i, j).Text Then 
                count += 1
            Else
                Exit For
            End If
            If count >= 3 Then
                winner = True
                MessageBox.Show("Player " & player & " wins!")
                ResetGame()
                Return
            End If
        Next

        ' Check diagonal (top-left to bottom-right)
        count = 0
        For k As Integer = 0 To 6
            If i + k > 6 Or j - k < 0 Then Exit For
            If gameboard(i + k, j - k).Text = gameboard(i, j).Text Then
                count += 1
            Else
                count = 0
            End If
            If count >= 4 Then
                winner = True
                MessageBox.Show("Player " & player & " wins!")
                ResetGame()
                Return
            End If
        Next

        ' Check diagonal (top-right to bottom-left)
        For k As Integer = 1 To 6
            If i - k < 0 Or j + k > 6 Then Exit For
            If gameboard(i - k, j + k).Text = gameboard(i, j).Text Then
                count += 1
            Else
                Exit For
            End If
            If count >= 3 Then
                winner = True
                MessageBox.Show("Player " & player & " wins!")
                ResetGame()
                Return
            End If
        Next

        ' Check for a tie game
        Dim filledCells As Integer = 0
        For i = 0 To 6
            For j = 0 To 6
                If gameboard(i, j).Text <> "" Then
                    filledCells += 1
                End If
            Next
        Next
        If filledCells = 49 Then ' all cells are filled
            winner = True
            MessageBox.Show("Tie game!")
            ResetGame()
        End If
    End Sub


    Private Sub ResetGame()
        ' Clear the board and reset the player
        For i As Integer = 0 To 6
            For j As Integer = 0 To 6
                gameboard(i, j).Text = ""
                gameboard(i, j).BackColor = Color.White ' set the background color to white
            Next
        Next
        player = 2
        winner = False
    End Sub

    Private Sub BtnSaveGame_Click(sender As Object, e As EventArgs) Handles BtnSaveGame.Click
        ' Create a SaveFileDialog to prompt the user for a file name
        Dim saveDialog As New SaveFileDialog()
        saveDialog.Filter = "Connect Four game files (*.cf)|*.cf"
        If saveDialog.ShowDialog() <> DialogResult.OK Then
            Return
        End If

        ' Open a FileStream to write the game state to the selected file
        Try
            Using fs As FileStream = File.Create(saveDialog.FileName)
                ' Write the player number and the state of each button to the file
                Using writer As New StreamWriter(fs)
                    writer.WriteLine(player)
                    For i As Integer = 0 To 6
                        For j As Integer = 0 To 6
                            If gameboard(i, j).Text = "" Then
                                writer.Write("-")
                            Else
                                writer.Write(gameboard(i, j).Text)
                            End If
                        Next
                        writer.WriteLine()
                    Next
                End Using
            End Using
            MessageBox.Show("Game saved successfully.")
        Catch ex As Exception
            MessageBox.Show("Error saving game: " & ex.Message)
        End Try
    End Sub

    Private Sub btnloadgame_Click(sender As Object, e As EventArgs) Handles btnloadgame.Click
        ' Prompt the user to confirm if they want to reset the board before loading a new game
        Dim result As DialogResult = MessageBox.Show("Do you want to reset the board before loading a new game?", "Load Game", MessageBoxButtons.YesNoCancel)

        If result = DialogResult.Yes Then
            ' Reset the board before loading the new game
            ResetGame()
        ElseIf result = DialogResult.Cancel Then
            ' Cancel the load operation if the user chooses to cancel
            Return
        End If

        ' Create an OpenFileDialog to prompt the user for a file to load
        Dim openDialog As New OpenFileDialog()
        openDialog.Filter = "Connect Four game files (*.cf)|*.cf"
        If openDialog.ShowDialog() <> DialogResult.OK Then
            Return
        End If

        ' Open the selected file and read the game state
        Try
            Using fs As FileStream = File.OpenRead(openDialog.FileName)
                Using reader As New StreamReader(fs)
                    ' Read the player number and update the current player
                    player = CInt(reader.ReadLine())

                    ' Read the state of each button and update the board
                    For i As Integer = 0 To 6
                        Dim line As String = reader.ReadLine()
                        For j As Integer = 0 To 6
                            If line(j) = "-" Then
                                gameboard(i, j).Text = ""
                            ElseIf line(j) = "1" Then
                                gameboard(i, j).Text = "1"
                                gameboard(i, j).BackColor = Color.Orange
                            ElseIf line(j) = "2" Then
                                gameboard(i, j).Text = "2"
                                gameboard(i, j).BackColor = Color.GreenYellow
                            End If
                        Next
                    Next
                End Using
            End Using
            MessageBox.Show("Game loaded successfully.")
        Catch ex As Exception
            MessageBox.Show("Error loading game: " & ex.Message)
        End Try
    End Sub

    Private Sub btnResetGame_Click(sender As Object, e As EventArgs) Handles btnResetGame.Click
        ResetGame()
        LabelPlayer.Text = "Player 1's turn"
        player = 1
        winner = False

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then Application.Exit()
    End Sub


End Class