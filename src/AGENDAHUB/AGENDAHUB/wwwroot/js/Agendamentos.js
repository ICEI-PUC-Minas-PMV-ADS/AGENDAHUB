document.addEventListener('DOMContentLoaded', function () {

    var statusFilter = document.getElementById('statusFilter');
    var cards = document.querySelectorAll('.card');

    statusFilter.addEventListener('change', function () {
        var selectedStatus = statusFilter.options[statusFilter.selectedIndex].value;

        cards.forEach(function (card) {
            var cardStatus = card.getAttribute('data-status');
            var shouldDisplay = selectedStatus === 'all' || cardStatus === selectedStatus;

            card.style.display = shouldDisplay ? 'block' : 'none';
        });
    });
    setTimeout(function () {
        var messageConcluido = document.getElementById("messageConcluido");
        if (messageConcluido) {
            messageConcluido.style.display = "none";
        }
    }, 2000); // 2000 milissegundos (2 segundos)
});
