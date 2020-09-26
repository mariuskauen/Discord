function updateScroll() {
    var element = document.getElementById("chatWindow");
    element.scrollTop = element.scrollHeight;
}

function getTokenFromStorage() {
    return localStorage.getItem("token");
}

function setTokenToStorage(token) {
    localStorage.setItem("token", token);
}

function parseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));
    return JSON.parse(jsonPayload);
}
function setUsernameToStorage(username) {
    localStorage.setItem("username", username);
}
function getUsernameFromStorage() {
    return localStorage.getItem("username");
}
