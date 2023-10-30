function abrirModal() {
    const modal = document.getElementById('janela-modal')
   modal.classList.add('abrir')

    modal.addEventListener('click', (e) => {
        if (e.target.id == 'janela-modal') {
            modal.classList.remove('abrir')
        }
    })

}

