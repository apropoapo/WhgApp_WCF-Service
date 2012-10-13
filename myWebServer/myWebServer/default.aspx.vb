Imports System.Net
Imports System.Net.Sockets
Imports System.Threading


Public Class _default
    Inherits System.Web.UI.Page

    Const port As Integer = 11000
    Public Shared allDone As New ManualResetEvent(False)
    Public Shared receivedMsg As New ManualResetEvent(True)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim ipHostInfo As IPHostEntry = Dns.Resolve(Dns.GetHostName())
        Dim ipAddress As IPAddress = ipHostInfo.AddressList(0)
        lstBoxLog.Items.Add(ipAddress.ToString)
        startAsynServer()
    End Sub

    Public Sub startAsynServer()
        Dim ipHostInfo As IPHostEntry = Dns.Resolve(Dns.GetHostName())
        Dim ipAddress As IPAddress = ipHostInfo.AddressList(0)
        Dim localEndPoint As New IPEndPoint(ipAddress, port)

        Dim listener = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)

        listener.Bind(localEndPoint)
        listener.Listen(100)

        'lstBoxLog.Items.Add("Status: Waiting for Connection...")

        Dim acceptingThread As New Thread(AddressOf acceptingThreadMethod)
        acceptingThread.Start(listener)



    End Sub

    Public Sub acceptingThreadMethod(ByVal listener As Socket)
        While True
            lstBoxLog.Items.Add("Status: Waiting for Connection...")
            allDone.Reset()
            listener.BeginAccept(New AsyncCallback(AddressOf AcceptCallback), listener)
            allDone.WaitOne()
        End While
    End Sub



    Private Sub AcceptCallback(ByVal ar As IAsyncResult)
        Dim listener As Socket = CType(ar.AsyncState, Socket)

        Dim handler = listener.EndAccept(ar)

        Dim state As New StateObject
        state.workSocket = handler

        'connected = True
        ' lblStatus.Text = "Status: Connected = " & connected & " with " & state.workSocket.RemoteEndPoint.ToString
        lstBoxLog.Items.Add("Status: Connected with " & state.workSocket.RemoteEndPoint.ToString)


        While True
            receivedMsg.Reset()
            lstBoxLog.Items.Add("wait for msg...")
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, New AsyncCallback(AddressOf ReadCallback), state)
            receivedMsg.WaitOne()
        End While
    End Sub


    Private Sub ReadCallback(ByVal ar As IAsyncResult)
        Dim data As String = String.Empty

        Dim state As StateObject = CType(ar.AsyncState, StateObject)
        Dim handler As Socket = state.workSocket
        Dim bytesRead As Integer = handler.EndReceive(ar)



        ' An incoming connection needs to be processed.
        If bytesRead > 0 Then
            data = Encoding.Unicode.GetString(state.buffer, 0, bytesRead)
            If data.IndexOf("<EOF>") > -1 Then
                lstBoxLog.Items.Add(data)
            Else
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, New AsyncCallback(AddressOf ReadCallback), state)

            End If
        End If
        receivedMsg.Set()
        allDone.Set()
    End Sub

End Class


Public Class StateObject
    ' Client  socket.
    Public workSocket As Socket = Nothing
    ' Size of receive buffer.
    Public Const BufferSize As Integer = 1024
    ' Receive buffer.
    Public buffer(BufferSize) As Byte
    ' Received data string.
    Public sb As New StringBuilder
End Class 'StateObject