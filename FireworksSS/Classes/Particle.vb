'--------------------------------------------------------------------------------------------------
' FireworksSS: Particle.vb
'    © 2026 Remus Rigo
'       v1.0.20261104
'--------------------------------------------------------------------------------------------------

Public Class Particle

   Public X As Single
   Public Y As Single
   Public VX As Single
   Public VY As Single

   Public ParticleColor As Color
   Public Life As Single = 1.0F      ' 1 = fresh, 0 = dead
   Public Decay As Single            ' life lost per tick
   Public Scale As Single = 1.0F     ' < 1 in preview mode

   '-----------------------------------------------------------------------------------------------
   ' Constructor
   Public Sub New(x As Single, y As Single, vx As Single, vy As Single, c As Color, decay As Single, Optional scale As Single = 1.0F)
      Me.X = x
      Me.Y = y
      Me.VX = vx
      Me.VY = vy
      Me.ParticleColor = c
      Me.Decay = decay
      Me.Scale = scale
   End Sub

   Public ReadOnly Property IsDead As Boolean
      Get
         Return Life <= 0
      End Get
   End Property

   '-----------------------------------------------------------------------------------------------
   ' Update the particle's position and life
   Public Sub Update()
      VX *= 0.98F          ' air drag
      VY *= 0.98F
      VY += 0.06F * Scale  ' gravity
      X += VX
      Y += VY
      Life -= Decay
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' Draw the particle
   Public Sub Draw(g As Graphics, b As SolidBrush)
      If Life <= 0 Then Return
      Dim alpha As Integer = CInt(255 * Math.Min(1.0F, Life))
      b.Color = Color.FromArgb(alpha, ParticleColor)
      Dim size As Single = Math.Max(1.0F, (2.0F + 1.5F * Life) * Scale)
      g.FillEllipse(b, X - size / 2, Y - size / 2, size, size)
   End Sub

End Class