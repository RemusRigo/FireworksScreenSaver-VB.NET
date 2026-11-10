<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCfg
   Inherits System.Windows.Forms.Form

   'Form overrides dispose to clean up the component list.
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

   'Required by the Windows Form Designer
   Private components As System.ComponentModel.IContainer

   'NOTE: The following procedure is required by the Windows Form Designer
   'It can be modified using the Windows Form Designer.
   'Do not modify it using the code editor.
   <System.Diagnostics.DebuggerStepThrough()> _
   Private Sub InitializeComponent()
      lblLaunchRate = New Label()
      nudLaunchRate = New NumericUpDown()
      lblLaunchRateUnit = New Label()
      lblBurstSize = New Label()
      nudBurstSize = New NumericUpDown()
      lblBurstSizeUnit = New Label()
      lblParticleLife = New Label()
      nudParticleLife = New NumericUpDown()
      lblParticleLifeUnit = New Label()
      btnOk = New Button()
      btnCancel = New Button()
      CType(nudLaunchRate, ComponentModel.ISupportInitialize).BeginInit()
      CType(nudBurstSize, ComponentModel.ISupportInitialize).BeginInit()
      CType(nudParticleLife, ComponentModel.ISupportInitialize).BeginInit()
      SuspendLayout()
      ' 
      ' lblLaunchRate
      ' 
      lblLaunchRate.AutoSize = True
      lblLaunchRate.Location = New Point(12, 20)
      lblLaunchRate.Name = "lblLaunchRate"
      lblLaunchRate.Size = New Size(72, 15)
      lblLaunchRate.TabIndex = 0
      lblLaunchRate.Text = "Launch rate:"
      ' 
      ' nudLaunchRate
      ' 
      nudLaunchRate.Location = New Point(110, 18)
      nudLaunchRate.Name = "nudLaunchRate"
      nudLaunchRate.Size = New Size(80, 23)
      nudLaunchRate.TabIndex = 1
      ' 
      ' lblLaunchRateUnit
      ' 
      lblLaunchRateUnit.AutoSize = True
      lblLaunchRateUnit.Location = New Point(196, 20)
      lblLaunchRateUnit.Name = "lblLaunchRateUnit"
      lblLaunchRateUnit.Size = New Size(94, 15)
      lblLaunchRateUnit.TabIndex = 2
      lblLaunchRateUnit.Text = "rockets / second"
      ' 
      ' lblBurstSize
      ' 
      lblBurstSize.AutoSize = True
      lblBurstSize.Location = New Point(12, 54)
      lblBurstSize.Name = "lblBurstSize"
      lblBurstSize.Size = New Size(59, 15)
      lblBurstSize.TabIndex = 3
      lblBurstSize.Text = "Burst size:"
      ' 
      ' nudBurstSize
      ' 
      nudBurstSize.Location = New Point(110, 52)
      nudBurstSize.Name = "nudBurstSize"
      nudBurstSize.Size = New Size(80, 23)
      nudBurstSize.TabIndex = 4
      ' 
      ' lblBurstSizeUnit
      ' 
      lblBurstSizeUnit.AutoSize = True
      lblBurstSizeUnit.Location = New Point(196, 54)
      lblBurstSizeUnit.Name = "lblBurstSizeUnit"
      lblBurstSizeUnit.Size = New Size(51, 15)
      lblBurstSizeUnit.TabIndex = 5
      lblBurstSizeUnit.Text = "particles"
      ' 
      ' lblParticleLife
      ' 
      lblParticleLife.AutoSize = True
      lblParticleLife.Location = New Point(12, 88)
      lblParticleLife.Name = "lblParticleLife"
      lblParticleLife.Size = New Size(68, 15)
      lblParticleLife.TabIndex = 6
      lblParticleLife.Text = "Particle life:"
      ' 
      ' nudParticleLife
      ' 
      nudParticleLife.Location = New Point(110, 86)
      nudParticleLife.Name = "nudParticleLife"
      nudParticleLife.Size = New Size(80, 23)
      nudParticleLife.TabIndex = 7
      ' 
      ' lblParticleLifeUnit
      ' 
      lblParticleLifeUnit.AutoSize = True
      lblParticleLifeUnit.Location = New Point(196, 88)
      lblParticleLifeUnit.Name = "lblParticleLifeUnit"
      lblParticleLifeUnit.Size = New Size(73, 15)
      lblParticleLifeUnit.TabIndex = 8
      lblParticleLifeUnit.Text = "milliseconds"
      ' 
      ' btnOk
      ' 
      btnOk.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
      btnOk.DialogResult = DialogResult.OK
      btnOk.Location = New Point(249, 110)
      btnOk.Name = "btnOk"
      btnOk.Size = New Size(35, 23)
      btnOk.TabIndex = 9
      btnOk.Text = "&Ok"
      btnOk.UseVisualStyleBackColor = True
      ' 
      ' btnCancel
      ' 
      btnCancel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
      btnCancel.DialogResult = DialogResult.Cancel
      btnCancel.Location = New Point(290, 110)
      btnCancel.Name = "btnCancel"
      btnCancel.Size = New Size(60, 23)
      btnCancel.TabIndex = 10
      btnCancel.Text = "&Cancel"
      btnCancel.UseVisualStyleBackColor = True
      ' 
      ' frmCfg
      ' 
      AcceptButton = btnOk
      AutoScaleDimensions = New SizeF(7F, 15F)
      AutoScaleMode = AutoScaleMode.Font
      CancelButton = btnCancel
      ClientSize = New Size(352, 136)
      Controls.Add(btnCancel)
      Controls.Add(btnOk)
      Controls.Add(lblParticleLifeUnit)
      Controls.Add(nudParticleLife)
      Controls.Add(lblParticleLife)
      Controls.Add(lblBurstSizeUnit)
      Controls.Add(nudBurstSize)
      Controls.Add(lblBurstSize)
      Controls.Add(lblLaunchRateUnit)
      Controls.Add(nudLaunchRate)
      Controls.Add(lblLaunchRate)
      FormBorderStyle = FormBorderStyle.FixedDialog
      MaximizeBox = False
      MinimizeBox = False
      Name = "frmCfg"
      Text = "Settings"
      CType(nudLaunchRate, ComponentModel.ISupportInitialize).EndInit()
      CType(nudBurstSize, ComponentModel.ISupportInitialize).EndInit()
      CType(nudParticleLife, ComponentModel.ISupportInitialize).EndInit()
      ResumeLayout(False)
      PerformLayout()
   End Sub

   Friend WithEvents lblLaunchRate As Label
   Friend WithEvents nudLaunchRate As NumericUpDown
   Friend WithEvents lblLaunchRateUnit As Label
   Friend WithEvents lblBurstSize As Label
   Friend WithEvents nudBurstSize As NumericUpDown
   Friend WithEvents lblBurstSizeUnit As Label
   Friend WithEvents lblParticleLife As Label
   Friend WithEvents nudParticleLife As NumericUpDown
   Friend WithEvents lblParticleLifeUnit As Label
   Friend WithEvents btnOk As Button
   Friend WithEvents btnCancel As Button
End Class
