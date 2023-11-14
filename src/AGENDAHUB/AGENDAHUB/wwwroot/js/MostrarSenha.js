function MostrarSenha() {
    var senha = document.getElementById('textSenha');
    senha.type = senha.type === 'password' ? 'text' : 'password';
}