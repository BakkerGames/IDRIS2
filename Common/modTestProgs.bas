Attribute VB_Name = "modTestProgs"
' ---------------------------------
' --- modTestProgs - 09/29/2008 ---
' ---------------------------------

Option Explicit

Public SortCompares As Long
Public SortSwaps As Long
Public SortTriples As Long

Public Sub MemorySortTest()

   Dim lngLoop As Long
   ' -----------------
   
   Rnd -123456
   For lngLoop = 1 To MAXMEMTAGS
      SortTags(lngLoop) = FormatNum("z8", Int(Rnd * 100000000))
   Next
   
   SortCompares = 0
   SortSwaps = 0
   For lngLoop = 1 To MAXMEMTAGS
      SortIndex(lngLoop) = lngLoop
   Next
   QuickSort0 1, MAXMEMTAGS
   Debug.Print "Compares: " & FormatNum(6, SortCompares) & _
               " - Swaps: " & FormatNum(6, SortSwaps);
   For lngLoop = 2 To MAXMEMTAGS
      If SortTags(SortIndex(lngLoop)) < SortTags(SortIndex(lngLoop - 1)) Then
         Debug.Print "Error!"
         End
      End If
   Next
   Debug.Print " - Ok"
   
   SortCompares = 0
   SortSwaps = 0
   For lngLoop = 1 To MAXMEMTAGS
      SortIndex(lngLoop) = lngLoop
   Next
   MemorySort0 1, MAXMEMTAGS
   Debug.Print "Compares: " & FormatNum(6, SortCompares) & _
               " - Swaps: " & FormatNum(6, SortSwaps);
   For lngLoop = 2 To MAXMEMTAGS
      If SortTags(SortIndex(lngLoop)) < SortTags(SortIndex(lngLoop - 1)) Then
         Debug.Print "Error!"
         End
      End If
   Next
   Debug.Print " - Ok"
   
   SortCompares = 0
   SortSwaps = 0
   SortTriples = 0
   For lngLoop = 1 To MAXMEMTAGS
      SortIndex(lngLoop) = lngLoop
   Next
   MemorySort1 1, MAXMEMTAGS
   Debug.Print "Compares: " & FormatNum(6, SortCompares) & _
               " - Swaps: " & FormatNum(6, SortSwaps) & _
               " - Triple swaps: " & FormatNum(6, SortTriples);
   For lngLoop = 2 To MAXMEMTAGS
      If SortTags(SortIndex(lngLoop)) < SortTags(SortIndex(lngLoop - 1)) Then
         Debug.Print "Error!"
         End
      End If
   Next
   Debug.Print " - Ok"
   
End Sub

Public Sub QuickSort0(ByVal L As Long, ByVal R As Long)
   Dim i As Long
   Dim j As Long
   Dim X As String
   Dim Y As Long
   ' -------------
   i = L
   j = R
   X = SortTags(SortIndex((L + R) / 2))
   Do While (i <= j)
      SortCompares = SortCompares + 1
      Do While (SortTags(SortIndex(i)) < X And i < R)
         i = i + 1
         If i = R Then Exit Do
         SortCompares = SortCompares + 1
      Loop
      SortCompares = SortCompares + 1
      Do While (X < SortTags(SortIndex(j)) And j > L)
         j = j - 1
         If j = L Then Exit Do
         SortCompares = SortCompares + 1
      Loop
      If (i < j) Then
         SortSwaps = SortSwaps + 1
         Y = SortIndex(i)
         SortIndex(i) = SortIndex(j)
         SortIndex(j) = Y
      End If
      If (i <= j) Then
         i = i + 1
         j = j - 1
      End If
   Loop
   If (L < j) Then QuickSort0 L, j
   If (i < R) Then QuickSort0 i, R
End Sub

Public Sub MemorySort0(ByVal FromPoint As Long, ByVal ToPoint As Long)
   ' --- This will swap elements until the middle element belongs in the middle. ---
   ' --- Everything before will be less and everything after will be greater.    ---
   Dim LowPtr As Long
   Dim HighPtr As Long
   Dim lngTemp As Long
   Dim MidPoint As Long
   ' ------------------
   ' --- check if nothing to do ---
   If FromPoint >= ToPoint Then Exit Sub
   ' --- get starting values ---
   MidPoint = (FromPoint + ToPoint) \ 2
   LowPtr = FromPoint
   HighPtr = ToPoint
   ' --- process until both end up at MidPoint ---
   Do While LowPtr < HighPtr
      ' --- find beginning and end of section needing sorting ---
      SortCompares = SortCompares + 1
      Do While LowPtr < MidPoint And SortTags(SortIndex(LowPtr)) <= SortTags(SortIndex(MidPoint))
         LowPtr = LowPtr + 1
         If LowPtr = MidPoint Then Exit Do
         SortCompares = SortCompares + 1
      Loop
      SortCompares = SortCompares + 1
      Do While HighPtr > MidPoint And SortTags(SortIndex(HighPtr)) >= SortTags(SortIndex(MidPoint))
         HighPtr = HighPtr - 1
         If HighPtr = MidPoint Then Exit Do
         SortCompares = SortCompares + 1
      Loop
      ' --- swap out-of-order elements ---
      If LowPtr < HighPtr Then
         lngTemp = SortIndex(LowPtr)
         SortIndex(LowPtr) = SortIndex(HighPtr)
         SortIndex(HighPtr) = lngTemp
         SortSwaps = SortSwaps + 1
         If LowPtr = MidPoint Then
            ' --- must start lower section over ---
            LowPtr = FromPoint
            HighPtr = HighPtr - 1
         ElseIf HighPtr = MidPoint Then
            ' --- must start higher section over ---
            LowPtr = LowPtr + 1
            HighPtr = ToPoint
         Else
            ' --- have handled these two positions ---
            LowPtr = LowPtr + 1
            HighPtr = HighPtr - 1
         End If
      End If
   Loop
   ' --- At this point, it is known that all values before MidPoint are less    ---
   ' --- than MidPoint and all values after MidPoint are greater than MidPoint. ---
   If FromPoint < MidPoint - 1 Then
      MemorySort0 FromPoint, MidPoint - 1
   End If
   If ToPoint > MidPoint + 1 Then
      MemorySort0 MidPoint + 1, ToPoint
   End If
End Sub

Public Sub MemorySort1(ByVal FromPoint As Long, ByVal ToPoint As Long)
   ' --- This will swap elements until the middle element belongs in the middle. ---
   ' --- Everything before will be less and everything after will be greater.    ---
   ' --- The actual middle point may shift by the end, so the two halves can be  ---
   ' --- different sizes. However, this allows many fewer compares in the first  ---
   ' --- two loops that find the beginning and end of each section.              ---
   Dim LowPtr As Long
   Dim HighPtr As Long
   Dim lngTemp As Long
   Dim MidPoint As Long
   ' ------------------
   ' --- check if nothing to do ---
   If FromPoint >= ToPoint Then Exit Sub
   ' --- get starting values ---
   MidPoint = (FromPoint + ToPoint) \ 2
   LowPtr = FromPoint
   HighPtr = ToPoint
   ' --- process until both end up at MidPoint ---
   Do While LowPtr < HighPtr
      ' --- find beginning and end of section needing sorting ---
      SortCompares = SortCompares + 1
      Do While LowPtr < MidPoint And SortTags(SortIndex(LowPtr)) <= SortTags(SortIndex(MidPoint))
         LowPtr = LowPtr + 1
         If LowPtr = MidPoint Then Exit Do
         SortCompares = SortCompares + 1
      Loop
      SortCompares = SortCompares + 1
      Do While HighPtr > MidPoint And SortTags(SortIndex(HighPtr)) >= SortTags(SortIndex(MidPoint))
         HighPtr = HighPtr - 1
         If HighPtr = MidPoint Then Exit Do
         SortCompares = SortCompares + 1
      Loop
      ' --- swap out-of-order elements ---
      If LowPtr < HighPtr Then
         If LowPtr = MidPoint Then
            ' --- know that we don't need to re-check low data ---
            lngTemp = SortIndex(MidPoint)
            SortIndex(MidPoint) = SortIndex(HighPtr)
            MidPoint = MidPoint + 1
            SortIndex(HighPtr) = SortIndex(MidPoint)
            SortIndex(MidPoint) = lngTemp
            LowPtr = LowPtr + 1
            SortTriples = SortTriples + 1
         ElseIf HighPtr = MidPoint Then
            ' --- know that we don't need to re-check high data ---
            lngTemp = SortIndex(MidPoint)
            SortIndex(MidPoint) = SortIndex(LowPtr)
            MidPoint = MidPoint - 1
            SortIndex(LowPtr) = SortIndex(MidPoint)
            SortIndex(MidPoint) = lngTemp
            HighPtr = HighPtr - 1
            SortTriples = SortTriples + 1
         Else
            lngTemp = SortIndex(LowPtr)
            SortIndex(LowPtr) = SortIndex(HighPtr)
            SortIndex(HighPtr) = lngTemp
            LowPtr = LowPtr + 1
            HighPtr = HighPtr - 1
            SortSwaps = SortSwaps + 1
         End If
      End If
   Loop
   ' --- At this point, it is known that all values before MidPoint are less    ---
   ' --- than MidPoint and all values after MidPoint are greater than MidPoint. ---
   If FromPoint < MidPoint - 1 Then
      MemorySort1 FromPoint, MidPoint - 1
   End If
   If ToPoint > MidPoint + 1 Then
      MemorySort1 MidPoint + 1, ToPoint
   End If
End Sub

Public Sub ClientListTest()
   ClientList = "0264"
   MsgBox GetClientWhere("CLIENT"), vbOKOnly, "CLIENT"
End Sub
