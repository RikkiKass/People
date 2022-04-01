$(() => {

    loadPeople();

    function loadPeople() {
        $.get('/people/getall', function (people) {
            $("#people-table tr:gt(0)").remove();
            people.forEach(person => {
                $("#people-table tbody").append(`
<tr>
    <td>${person.firstName}</td>
    <td>${person.lastName}</td>
    <td>${person.age}</td>
    <td><button class='btn btn-primary' id='edit' data-id=${person.id} data-first-name=${person.firstName} data-last-name=${person.lastName} data-age=${person.age}>Edit</button></td>
    <td><button class='btn btn-danger' id='delete' data-id=${person.id}>Delete</button></td>
</tr>`);
            });
        });
    }

    $("#add-person").on('click', function () {
        const firstName = $("#first-name").val();
        const lastName = $("#last-name").val();
        const age = $("#age").val();


        $.post('/people/addperson', { firstName, lastName, age }, function (person) {

            loadPeople();
            $("#first-name").val('');
            $("#last-name").val('');
            $("#age").val('');
        });
    });


    $("#people-table").on('click', "#edit", function () {
        const button = $(this);
        const id = button.data('id');
        const firstName = button.data('first-name');
        const lastName = button.data('last-name');
        const age = button.data('age');

        $("#edit-first-name").val(firstName);
        $("#edit-last-name").val(lastName);
        $("#edit-age").val(age);
        $("#edit-id").val(id);
        $("#name").text(`${firstName} ${lastName}`);

      
        $(".modal").modal();

        $("#btn-save").on('click', function () {
            const firstName = $("#edit-first-name").val();
            const lastName = $("#edit-last-name").val();
            const age = $("#edit-age").val();
            const id = $("#edit-id").val();
            $.post('/people/edit', { id , firstName, lastName, age}, function () {
                loadPeople();
            });
            $(".modal").modal('hide');
        });

    });


    $("#people-table").on('click', "#delete", function () {
        const button = $(this);
        const id = button.data('id');
        console.log(id);
        $.post('/people/delete', { id }, function () {
            loadPeople();
        });
    });
});