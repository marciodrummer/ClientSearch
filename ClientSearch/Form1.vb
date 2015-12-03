Public Class Form1
    Private Sub NewClientToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewClientToolStripMenuItem.Click

        If sender.ToString() = "New Client" Then
            Dim strPath As String

            Call LoadFile()
        End If


    End Sub

    Private Sub LoadFile()
        Dim myStream As IO.Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()
        Dim strPath As String
        Dim strClient As String
        Dim MyConnection As System.Data.OleDb.OleDbConnection
        Dim DtSet As System.Data.DataSet
        Dim MyCommand As System.Data.OleDb.OleDbDataAdapter


        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "xls files (*.xls)|*.xls"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        strClient = InputBox("Enter Client Name:", "Client")

        If Trim(strClient) <> "" Then

            If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Try
                    If openFileDialog1.CheckPathExists = True Then
                        strPath = openFileDialog1.FileName
                        MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & strPath & "';Extended Properties=Excel 8.0;")
                        MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [Plan1$]", MyConnection)
                        MyCommand.TableMappings.Add("Table", "Net-informations.com")
                        DtSet = New System.Data.DataSet
                        MyCommand.Fill(DtSet)
                        dgvNewClient.DataSource = DtSet.Tables(0)
                        MyConnection.Close()
                    End If
                Catch Ex As Exception
                    MessageBox.Show("Cannot read file from disk. Original error: " & Ex.Message)
                Finally
                    ' Check this again, since we need to make sure we didn't throw an exception on open.
                    If (myStream IsNot Nothing) Then
                        myStream.Close()
                    End If
                End Try
            End If
        End If
    End Sub


End Class
