  function createAlert() {
    alert("hey this is an alert!")
}
function AskQuestion(messages) {
    return prompt(messages);
}
function setElementById(id,Text) {
    document.getElementById(id).innerText=Text;
}
function focus(emelemt) {
    emelemt.focus();
}
  function GetMessage(target) {
      return "Hello " + target + "!!!";
  }