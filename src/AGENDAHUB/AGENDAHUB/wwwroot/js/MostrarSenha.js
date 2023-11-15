// MostrarSenha.js
function MostrarSenha() {
    var senhaInput = document.getElementById('textSenha');
    var iconeSenha = document.getElementById('iconeSenha');

    if (senhaInput.type === 'password') {
        senhaInput.type = 'text';
        iconeSenha.src = '/images/Eye.png';
    } else {
        senhaInput.type = 'password';
        iconeSenha.src = '/images/hideEye.png';
    }
}

