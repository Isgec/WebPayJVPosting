Imports System.Text
Imports System.Xml
Public Class JVPostingMain

  Private Sub cmdBrowseJVTextFile_Click(sender As System.Object, e As System.EventArgs) Handles cmdBrowseJVTextFile.Click
    If txtJVFile.Text <> String.Empty Then
      ofd1.InitialDirectory = IO.Path.GetFullPath(txtJVFile.Text)
    End If
    ofd1.ShowDialog()
    If ofd1.FileName <> String.Empty Then
      txtJVFile.Text = ofd1.FileName
    End If
  End Sub

  Private Sub cmdConvertPSV_Click(sender As System.Object, e As System.EventArgs) Handles cmdConvertPSV.Click
    If txtJVFile.Text = String.Empty Then
      MsgBox("No file specified.")
      Exit Sub
    End If
    Dim tr As IO.StreamReader = New IO.StreamReader(txtJVFile.Text)
    Dim tw As IO.StreamWriter = New IO.StreamWriter(IO.Path.GetDirectoryName(txtJVFile.Text) & "\" & IO.Path.GetFileNameWithoutExtension(txtJVFile.Text) & ".psv")
    Dim str1 As String = tr.ReadLine
    Do While str1 IsNot Nothing

      str1 = str1.Trim
      If str1.Length >= 106 Then
        str1 = str1.Insert(101, "|")
      End If
      str1 = str1.Insert(78, "|")
      str1 = str1.Insert(48, "|")
      str1 = str1.Insert(47, "|")
      str1 = str1.Insert(26, "|")
      str1 = str1.Insert(21, "|")
      str1 = str1.Insert(15, "|")
      str1 = str1.Insert(3, "|")
      tw.WriteLine(str1)
      str1 = tr.ReadLine
    Loop
    tr.Close()
    tw.Close()
  End Sub

  Delegate Sub tmp(ByVal jvFile As String, ByVal bar As ProgressBar, ByVal lbl As Label, ByVal splitit As Boolean)
  Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
    If txtJVFile.Text = String.Empty Then
      MsgBox("No file specified.")
      Exit Sub
    End If
    Dim ds As tmp = AddressOf ConvertData
    ds.BeginInvoke(txtJVFile.Text, pBar, pLbl, chkSplit.Checked, Nothing, Nothing)
  End Sub
  Private Sub ConvertData(ByVal jvFile As String, ByVal bar As ProgressBar, ByVal lbl As Label, ByVal splitit As Boolean)
    Dim oBj As ConvertSalaryData = New ConvertSalaryData(jvFile, pBar, pLbl, splitit)
    oBj.ConvertSalaryData()
  End Sub
End Class


Public Class ConvertSalaryData
  Sub New(ByVal jvFile As String, ByVal bar As ProgressBar, ByVal lbl As Label, ByVal SplitIt As Boolean)
    txtJVFile = jvFile
    pBar = bar
    pLbl = lbl
    chksplit = SplitIt
  End Sub
  Delegate Sub tmp1(ByVal str As String)
  Dim pBar As ProgressBar = Nothing
  Dim pLbl As Label = Nothing
  Dim txtJVFile As String = ""
  Dim chksplit As Boolean = False
  Public Sub ConvertSalaryData()
    Dim CardNo As String = "340"
    Dim psvFile As String = IO.Path.GetDirectoryName(txtJVFile) & "\" & IO.Path.GetFileNameWithoutExtension(txtJVFile) & ".psv"

    Dim oVchs As New List(Of Vouchers)
    Dim FileNo As Integer = 0
    Dim SplitXML As Boolean = False
    Dim SplitStart As Integer = 0
    Dim SplitEnd As Integer = 2000
    Dim srno As Integer = 0
    Dim TotLine As Integer = 0
    Dim pb As Integer = 0
    Dim pv As Integer = 0
    Dim aCmp() As String = Nothing
    Dim rstr() As String = Nothing
    Dim oTS As IO.StreamReader = New IO.StreamReader(psvFile)
    Dim tmp As String = oTS.ReadLine
    Do While tmp IsNot Nothing
      curVal(TotLine)
      TotLine += 1
      rstr = tmp.Split("|".ToCharArray)
      Try
        Dim Found As Boolean = False
        If aCmp Is Nothing Then
          ReDim aCmp(0)
          aCmp(0) = rstr(0)
        End If
        For Each ttmp As String In aCmp
          If ttmp.ToLower = rstr(0).ToLower Then
            Found = True
            Exit For
          End If
        Next
        If Not Found Then
          ReDim Preserve aCmp(aCmp.Length)
          aCmp(aCmp.Length - 1) = rstr(0)
        End If
      Catch ex As Exception
        curVal(TotLine & " : " & ex.Message)
        Threading.Thread.Sleep(12000)
      End Try
      tmp = oTS.ReadLine
    Loop
    oTS.Close()
    InitBar(TotLine)
    pv = TotLine
    For Each cmp As String In aCmp
      If cmp.ToLower = "ijt" Then
        If chksplit Then
          SplitXML = True
        Else
          SplitXML = False
        End If
      End If
      SplitStart = 0
      SplitEnd = 2000
      FileNo = 0
      'To execute twice split file below label is used
ExecuteSplit:
      pv = TotLine
      pb = 0
      srno = 0
      oVchs = New List(Of Vouchers)
      FileNo += 1
      oTS = New IO.StreamReader(psvFile)
      Dim rrStr As String = oTS.ReadLine
      Do While rrStr IsNot Nothing
        Try
          pb += 1
          pv -= 1
          curBar(pb)
          curVal(pv)
          rstr = rrStr.Split("|".ToCharArray)
          If rstr(0).ToLower <> cmp.ToLower Then
            rrStr = oTS.ReadLine
            Continue Do
          End If
          If SplitXML Then
            If Convert.ToInt32(rstr(3).Trim) < SplitStart Or Convert.ToInt32(rstr(3).Trim) > SplitEnd Then
              rrStr = oTS.ReadLine
              Continue Do
            End If
          End If
          Select Case rstr(0).ToUpper
            Case "IJT"
              rstr(0) = 200
            Case "HOC"
              rstr(0) = 400
            Case "RND"
              rstr(0) = 290
          End Select
          Dim oVch As New Vouchers
          srno += 1
          With oVch
            .BatchCompany = rstr(0)
            .TargetCompany = rstr(0)
            .ForSoftware = "10"
            rstr(7) = rstr(7).Trim
            .VoucherDate = rstr(7).Substring(6, 2) & "/" & rstr(7).Substring(4, 2) & "/" & rstr(7).Substring(0, 4)
            .ProcessID = ""
            .SerialNo = srno
            .BusinessPartner = ""
            .TranType = "JVN"
            .Series = ""
            .LedgerAc = Convert.ToInt64(rstr(1))
            .Dim1 = ""
            .Dim2 = rstr(2).ToString.ToUpper.Trim.Replace("&", "&amp;")
            .Dim3 = Convert.ToInt32(rstr(3))
            Try
              .Dim4 = rstr(8).Trim
            Catch ex As Exception
            End Try
            .Dim5 = "UP"
            .Currency = "INR"
            .Amount = rstr(4)
            .DrCr = IIf(rstr(5) = "C", 2, 1)
            .Remarks = rstr(6).Replace(",", " ")
            .CreatedBy = CardNo
            .BatchNo = ""
            .DocumentNo = ""
            .LineNo = ""
            .DocumentID = ""
          End With
          oVchs.Add(oVch)
          rrStr = oTS.ReadLine
        Catch ex As Exception
        End Try
      Loop
      oTS.Close()
      If oVchs.Count > 0 Then
        WriteSalaryXML(oVchs, FileNo, psvFile, cmp)
      End If
      If SplitXML Then
        SplitStart = SplitEnd + 1
        SplitEnd += 7999
        If SplitStart > TotLine Then
          Continue For
        End If
        GoTo ExecuteSplit
      End If
    Next
done:
    curVal("DONE")
    pBar = Nothing
    pLbl = Nothing
  End Sub
  Private Sub WriteSalaryXML(ByVal oVchs As List(Of Vouchers), ByVal FileNo As String, ByVal pName As String, ByVal cmp As String)
    Dim FName As String = IO.Path.GetFileNameWithoutExtension(pName)
    Dim FPath As String = IO.Path.GetDirectoryName(pName)
    Dim mFileName As String = "Salary_" & FileNo & "_" & cmp & "_" & FName.ToUpper & ".xml"
    Dim oTW As IO.StreamWriter = New IO.StreamWriter(FPath & "/" & mFileName)
    oTW.WriteLine("<?xml version=""1.0"" encoding=""utf-8""?>")

    oTW.WriteLine("<Companies>")
    oTW.WriteLine("  <Company name=""" & oVchs(0).BatchCompany & """>")
    oTW.WriteLine("  <Lines>")
    For Each oVch As Vouchers In oVchs
      oTW.WriteLine("      <Line PrkLedgerID=""" & oVch.DocumentID & """  ProcessID="""" SerialNo="""" BatchNo="""" DocumentNo="""" LineNo="""" GetBatchDocument=""" & oVch.BatchCompany & "," & oVch.ForSoftware & "," & oVch.VoucherDate & "," & "[ProcessID]" & "," & "[SerialNo]" & """>" & oVch.BatchCompany & "," & oVch.TargetCompany & "," & oVch.ForSoftware & "," & oVch.VoucherDate & "," & "[ProcessID]" & "," & oVch.SerialNo & "," & oVch.BusinessPartner & "," & oVch.TranType & "," & oVch.Series & "," & oVch.LedgerAc & "," & oVch.Dim1 & "," & oVch.Dim2 & "," & oVch.Dim3 & "," & oVch.Dim4 & "," & oVch.Dim5 & "," & oVch.Currency & "," & oVch.Amount & "," & oVch.DrCr & "," & oVch.Remarks & "," & oVch.CreatedBy & "," & oVch.CF & "</Line>")
    Next
    oTW.WriteLine("    </Lines>")
    oTW.WriteLine("    <Batch>" & oVchs(0).BatchCompany & "," & oVchs(0).ForSoftware & "," & oVchs(0).VoucherDate & "," & "[ProcessID]" & "," & oVchs(0).TranType & "</Batch>")
    oTW.WriteLine("    <Errors><Error></Error></Errors>")
    oTW.WriteLine("  </Company>")
    oTW.WriteLine("</Companies>")
    oTW.Close()
    Dim tr As IO.StreamReader = New IO.StreamReader(FPath & "/" & mFileName)
    Dim sb As StringBuilder = New StringBuilder
    Dim ch As Char
    Dim str As String = tr.ReadToEnd
    tr.Close()
    For Each ch In str
      If (XmlConvert.IsXmlChar(ch)) Then
        sb.Append(ch)
      End If
    Next
    oTW = New IO.StreamWriter(FPath & "/" & mFileName)
    oTW.Write(sb.ToString)
    oTW.Close()

  End Sub

  Private Class Vouchers
    Public BatchCompany As String
    Public ForSoftware As String
    Public ProcessID As String  ' Unique Number will be returned from BaaN, to use in subsiquent line
    Public VoucherDate As String
    Public SerialNo As String ' Will be returned from BaaN
    Public BusinessPartner As String
    Public LedgerAc As String
    Public Dim1 As String
    Public Dim2 As String
    Public Dim3 As String
    Public Dim4 As String
    Public Dim5 As String
    Public Currency As String 'INR
    Public Amount As String
    Public DrCr As String '1-Debit, 2-Credit
    Public TargetCompany As String 'Employees Company in case of Cash Vch
    Public Remarks As String '32 Chars
    Public TranType As String 'JVR,CSH,CS1, IMprest
    Public Series As String 'Blank to use default series from BaaN else specify to use series
    Public CreatedBy As String 'LoginID of voucher posting user
    Public BatchNo As String 'Will be returned from BaaN
    Public DocumentNo As String 'Will be returned from BaaN
    Public LineNo As String 'Will be returned from BaaN
    Public CF As Decimal = 1.0 'Conversion Factor

    'Added Fields
    Public DocumentID As String

  End Class
  Private Sub curVal(ByVal str As String)
    If pLbl.InvokeRequired Then
      pLbl.Invoke(New tmp1(AddressOf curVal), str)
    Else
      pLbl.Text = str
    End If
  End Sub
  Private Sub curBar(ByVal str As String)
    If pBar.InvokeRequired Then
      pBar.Invoke(New tmp1(AddressOf curBar), str)
    Else
      Try
        pBar.Value = str
      Catch ex As Exception
      End Try
    End If
  End Sub
  Private Sub InitBar(ByVal str As String)
    If pBar.InvokeRequired Then
      pBar.Invoke(New tmp1(AddressOf InitBar), str)
    Else
      pBar.Maximum = str
      pBar.Value = 0
    End If
  End Sub

End Class