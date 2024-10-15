// let is like var but local
let links = document.querySelectorAll(`a`) //querySelectorAll pulls all of () instead of first instance of
let sections = document.querySelectorAll(`section`)

console.log(links)

for(let i = 0; i < links.length; i++){
    links[i].addEventListener(`click`, function(e){
        e.preventDefault();

        //loop through the sections adding hide to everything
        for(let ii = 0; ii < sections.length; ii++){
            sections[ii].classList.add(`hide`)
        }

        //remove hide from the section in question
        sections[i].classList.remove(`hide`)
        
    })
}