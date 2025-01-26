/* Module Script */
var Hexstor = Hexstor || {};

Hexstor.Template = {

};

window.pauseAudio = (playerId) => {
    var audioElement = document.getElementById(playerId);
    if (audioElement) {
        audioElement.pause();
    }
}

window.playAudio = (playerId) => {
    var audioElement = document.getElementById(playerId);
    if (audioElement) {
        audioElement.play();
    }
}