const signalR = require("@microsoft/signalr");

let connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost/api/hamzacadhub", {
        accessTokenFactory:
            //jwt token
            () => "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImY3ZWUzYTYzLWMyNWQtNDY1NS1iNjM2LWRkNGY0OTMxYjJjNSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2RhdGVvZmJpcnRoIjoiMDgtMTAtMjAyMyAwMjoyMjo0NyIsImp0aSI6ImRjZjlmYzA3LTI4ODQtNDVlOS1iMjlmLTQ5ZTYzYzYxMzQ3OSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlVzZXIiLCJleHAiOjE2OTY3NzQ5NjcsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NjE5NTUiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQyMDAifQ.FxKJQoWHZtvnW996xqn9xH9ofUoVaYulEtI7fXUa_RI"
    })
    .build();

connection.on("send", data => {
    console.log(data);
});

connection.start()
    .then(async () => {
        try {
            await connection.invoke("SendMessage", "user", "message");
        } catch (err) {
            console.error(err);
        }
    });

connection.on("ReceiveMessage", (user, message) => {
    console.log(`${user}: ${message}`);
});