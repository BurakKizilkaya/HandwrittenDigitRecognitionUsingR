Imports System.Drawing
Imports System
Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Form1

    'creating input matrix, bitmap, and other variables for drawing
    Dim input(28, 28) As Integer
    Dim clr As Integer
    Dim draw As Boolean
    Dim drawcolor As Color = Color.White
    Dim bmp As Bitmap
    Dim str As String


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'populate pictureboximage property
        bmp = New Bitmap(pbDraw.Width, pbDraw.Height)
        pbDraw.Image = bmp

    End Sub

    Private Sub paintbrush(x As Integer, y As Integer)
        'this function draws the picturebox ,2 pixels for each mouse click
        Using g As Graphics = Graphics.FromImage(pbDraw.Image)
            g.FillRectangle(New SolidBrush(drawcolor), New Rectangle(x, y, 2, 2))
        End Using
        pbDraw.Refresh()

    End Sub

    Private Sub pbDraw_MouseDown(sender As Object, e As MouseEventArgs) Handles pbDraw.MouseDown
        'flag change according to mouse move
        draw = True
        paintbrush(e.X, e.Y)

    End Sub

    Private Sub pbDraw_MouseMove(sender As Object, e As MouseEventArgs) Handles pbDraw.MouseMove
        'in every move of mouse draw flag is checked if it is true, drawing is done
        If draw = True Then
            paintbrush(e.X, e.Y)
        End If

    End Sub

    Private Sub pbDraw_MouseUp(sender As Object, e As MouseEventArgs) Handles pbDraw.MouseUp
        'flag change according to mouse move
        draw = False

    End Sub

    Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        'when the clear button is pressed, picturebox is cleared
        bmp = New Bitmap(pbDraw.Width, pbDraw.Height)
        pbDraw.Image = bmp

    End Sub


    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        'drawing to bitmap and saving image as png for checking purposes
        pbDraw.DrawToBitmap(bmp, New Rectangle(0, 0, pbDraw.Width, pbDraw.Height))
        bmp.Save("C:/Users/labuser/Desktop/input.png", Imaging.ImageFormat.Png)


        'file stream for write is opened
        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter("C:/Users/labuser/Desktop/MNISTdatasetNEW/MNISTcsv/myinput.csv", True)

        'getting pixels of drawing
        For i = 0 To 27 'we have 28 X 28 pixel images
            For j = 0 To 27
                With bmp.GetPixel(j, i) 'taking each pixel
                    clr = .R
                    input(i, j) = clr 'populating input matix
                    str = str & input(i, j) & ","   'creating comma delimated string to save input to csv file
                End With
            Next j
        Next i
        str = str.Substring(0, str.Length - 1)

        file.WriteLine(str) 'writing input pixels to csv file
        file.Close()


        'printing input matrix to see content of matrix
        For a = 0 To 27
            For b = 0 To 27
                Label1.Text = Label1.Text & input(a, b) & " "
            Next
            Label1.Text = Label1.Text & vbNewLine
        Next

    End Sub
End Class
