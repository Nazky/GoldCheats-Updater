<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Credit
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Software : "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label2.Location = New System.Drawing.Point(77, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Nazky (@NazkyYT]"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Payload : "
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label4.Location = New System.Drawing.Point(77, 36)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "SISTRo (Sistr0)"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label5.Location = New System.Drawing.Point(96, 63)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(95, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Kme (@Kameleon)"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 63)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(87, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Cheats Update : "
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 92)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(86, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Cheats Creator : "
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(104, 92)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(133, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Can be find in the json files"
        '
        'Credit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(251, 118)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "Credit"
        Me.Opacity = 0.95R
        Me.ShowIcon = False
        Me.Text = "Credit"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
End Class
