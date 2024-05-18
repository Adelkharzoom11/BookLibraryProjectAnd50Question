document.addEventListener('DOMContentLoaded', function() {
    fetchQuestions();
});

function fetchQuestions() {
    fetch('https://api.stackexchange.com/2.3/questions?order=desc&sort=activity&site=stackoverflow')
        .then(response => response.json())
        .then(data => {
            displayQuestions(data.items);
        })
        .catch(error => console.error('Error fetching questions:', error));
}

function displayQuestions(questions) {
    const questionsList = document.getElementById('questionsList');
    questions.forEach(question => {
        const li = document.createElement('li');
        li.innerHTML = `<a href="details.html?questionId=${question.question_id}">${question.title}</a>`;
        questionsList.appendChild(li);
    });
}
