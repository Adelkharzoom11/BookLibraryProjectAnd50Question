document.addEventListener('DOMContentLoaded', function() {
    const urlParams = new URLSearchParams(window.location.search);
    const questionId = urlParams.get('questionId');
    fetchQuestionDetails(questionId);
});

function fetchQuestionDetails(questionId) {
    fetch(`https://api.stackexchange.com/2.3/questions/${questionId}?order=desc&sort=activity&site=stackoverflow&filter=withbody`)
        .then(response => response.json())
        .then(data => {
            displayQuestionDetails(data.items[0]);
        })
        .catch(error => console.error('Error fetching question details:', error));
}

function displayQuestionDetails(question) {
    document.getElementById('questionTitle').innerText = question.title;
    document.getElementById('questionBody').innerHTML = question.body;
}
