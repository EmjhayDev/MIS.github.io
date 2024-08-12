Public Class Win32API


    'Start Windows Form Animation
    'Usage: <Win32API.AnimateWindow(Me.Handle, 1000, Win32API.AW_BLEND)>
    Declare Function AnimateWindow Lib "user32" (ByVal hwnd As IntPtr, ByVal dwTime As Integer, ByVal dwFlags As Integer) As Boolean
    Public Const AW_HOR_POSITIVE As Integer = &H1 'Animates the window from left to right. This flag can be used with roll or slide animation.
    Public Const AW_HOR_NEGATIVE As Integer = &H2 'Animates the window from right to left. This flag can be used with roll or slide animation.
    Public Const AW_VER_POSITIVE As Integer = &H4 'Animates the window from top to bottom. This flag can be used with roll or slide animation.
    Public Const AW_VER_NEGATIVE As Integer = &H8 'Animates the window from bottom to top. This flag can be used with roll or slide animation.
    Public Const AW_CENTER As Integer = &H10 'Makes the window appear to collapse inward if AW_HIDE is used or expand outward if the AW_HIDE is not used.
    Public Const AW_HIDE As Integer = &H10000 'Hides the window. By default, the window is shown.
    Public Const AW_ACTIVATE As Integer = &H20000 'Activates the window.
    Public Const AW_SLIDE As Integer = &H40000 'Uses slide animation. By default, roll animation is used.
    Public Const AW_BLEND As Integer = &H80000 'Uses a fade effect. This flag can be used only if hwnd is a top-level window.


    'Start Move Windows Form
    'Usage: Win32API.MouseDown(Me,e)
    Dim x, y As Integer
    Dim NewPoint As New System.Drawing.Point

End Class
