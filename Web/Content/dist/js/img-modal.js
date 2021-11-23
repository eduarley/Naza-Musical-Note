//Get the modal
var modal = document.getElementById('imgModal');

//Get the image
var img = document.getElementById('photo');
var modalImg = document.getElementById('img01');
var captionText = document.getElementById("caption");

img.onclick = function(){
  modal.style.display = "block";
  modalImg.src = this.src;
  modalImg.alt = this.alt;
  captionText.style.display = "block";
}

var span = document.getElementsByClassName("close")[0];

//click on (x), close the modal
span.onclick = function() { 
    modal.style.display = "none";
}