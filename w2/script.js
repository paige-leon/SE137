//The links and section variables are lists containing all of the a tags and section tags
let links = document.querySelectorAll(`a`)
let section = document.querySelectorAll(`section`)

//Loop through the links and make them clickable
for(let i=0; i<links.length; i++)
{
    links[i].addEventListener(`click`, function(e){

        e.preventDefault();
        //add a class to every section that hides them
        for(let ii=0; ii<section.length; ii++)
        {
            section[ii].classList.add(`hide`)
        }
        //remove the hide class from 
        section[i].classList.remove(`hide`)
    })
}