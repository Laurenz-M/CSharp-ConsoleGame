//declaration of deck structure and content
const cards = {
    two: 2,
    three: 3,
    four: 4,
    five: 5,
    six: 6,
    seven: 7,
    eight: 8,
    nine: 9,
    ten: 10,
    jack: 10,
    queen: 10,
    king: 10,
    ace: 'ace'
}
const baseDeck = { }
const cardNames = Object.keys(cards)
const suitNames = ['clubs','hearts','spades','diamonds']
cardNames.forEach(cardName => {

    baseDeck[cardName] = {}
    baseDeck[cardName]['totalAmount'] = 4
    baseDeck[cardName]['cardValue'] = cards[cardName]
    baseDeck[cardName]['suits'] = {}
    suitNames.forEach(suitName => {
        baseDeck[cardName].suits[suitName] = 1
    })

})

//start of game
let dealerHand = []
let playerHand = []

let currentDeck = baseDeck
currentDeck = takeCard(randomDeckCard(currentDeck), currentDeck)
calculateHandValue([{ cardValue: 'ace', cardName: 'ace', suitName: 'diamonds' },{ cardValue: 'ace', cardName: 'ace', suitName: 'diamonds' },{ cardValue: 4, cardName: 'four', suitName: 'diamonds' }])


//functions
function randomDeckCard(deck){
    const cardAmount = Object.keys(deck).length - 1

    let pickedCardIndex = randomInRange(0,cardAmount)

    while(deck[Object.keys(deck)[pickedCardIndex]].totalAmount < 1){
        pickedCardIndex = randomInRange(0,cardAmount)
    }//picks new card until picked card is still present in deck

    const pickedCardName = Object.keys(deck)[pickedCardIndex]

    let pickedCardObject = deck[pickedCardName]

    //console.log(pickedCardName,Object.keys(pickedCardObject.suits).length)
    
    const suitAmount = Object.keys(pickedCardObject.suits).length - 1
    //console.log(suitAmount)
    //return
    let pickedSuitIndex = randomInRange(0,suitAmount)
    
    while(pickedCardObject.suits[Object.keys(pickedCardObject.suits)[pickedSuitIndex]] < 1){
        pickedCardIndex = randomInRange(0,cardAmount)
    }//picks new suit until picked suit of card is still present in deck
    
    const pickedSuitName = Object.keys(pickedCardObject.suits)[pickedSuitIndex]

    pickedCardObject['suitName'] = pickedSuitName
    pickedCardObject['cardName'] = pickedCardName

    const tempReturnObjectUntilFailureFound = {

        cardValue: '',
        cardName: '',
        suitName: '',

    }
    tempReturnObjectUntilFailureFound.cardName = pickedCardObject.cardName
    tempReturnObjectUntilFailureFound.suitName = pickedCardObject.suitName
    tempReturnObjectUntilFailureFound.cardValue = pickedCardObject.cardValue
    //delete pickedCardObject['suits']
    //delete pickedCardObject['totalAmount']
    console.log(tempReturnObjectUntilFailureFound)
    return tempReturnObjectUntilFailureFound

}

function randomInRange(min, max){
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

function takeCard(card,deck){

    deck[card.cardName].totalAmount--
    deck[card.cardName].suits[card.suitName]--
    return deck
}

function calculateHandValue(hand){

    let includesAce = false
    hand.forEach(card => {
        if(card.cardName === 'ace') includesAce = true
    })

    if(!includesAce){

        let sumAmount = 0
        hand.forEach(card => {
            sumAmount += card.cardValue
        })
        console.log(sumAmount)
        return sumAmount
    }
    else{
        let aceArray = []
        hand.forEach(card => {
            if(card.cardName === 'ace') aceArray.push(card)
        })
        restArray = hand.filter(card => card.cardName !== 'ace')
        console.log('aceArray', aceArray)
        console.log('restArray', restArray)

        let handWithoutAce = 0
        restArray.forEach(card => {
            handWithoutAce += card.cardValue
        })
        console.log(handWithoutAce)

        let addAceToValue = handWithoutAce
        aceArray.forEach(card => {

            if(addAceToValue + 11 > 21){
                //1
                addAceToValue += 1
                card.cardValue = 1
            }
            else if(addAceToValue + 11 <= 21){
                //11
                addAceToValue += 11
                card.cardValue = 11
            }
            else console.log('else')
        })
        console.log('aceToValue', addAceToValue)
        console.log('aceArray', aceArray)
        console.log('restArray', restArray)
    }

}

function calculateAce(){

}