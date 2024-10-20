using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public async Task CallUser(string userId, RTCSessionDescription offer)
    {
        await Clients.User(userId).SendAsync("ReceiveCall", Context.ConnectionId, offer);
    }

    public async Task AnswerCall(string callerId, RTCSessionDescription answer)
    {
        await Clients.Client(callerId).SendAsync("CallAnswered", Context.ConnectionId, answer);
    }

    public async Task SendIceCandidate(string userId, RTCIceCandidate candidate)
    {
        await Clients.User(userId).SendAsync("ReceiveIceCandidate", candidate);
    }
}

// Custom classes for RTC session description and ICE candidates
public class RTCSessionDescription
{
    public string Type { get; set; }
    public string Sdp { get; set; }
}

public class RTCIceCandidate
{
    public string Candidate { get; set; }
    public string SdpMid { get; set; }
    public int SdpMLineIndex { get; set; }
}
