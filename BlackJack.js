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

//const blackjackVariants = []
const splitRules = { 

    standard: {
        maxAllowedSplits: 2,
        canResplitAces: false,
        onlySplitIdenticalValueTenCards: false,
        multipleHitsOnSplitAce: false,
        canHitAndDoubleOnSplits: true
    },
    variations: {   //contains an array for each option that inc,udes all valid variations
        maxAllowedSplits: [2,3,-1], //"-1" stands for unlimited splits
        canResplitAces: [false, true],
        onlySplitIdenticalValueTenCards: [false, true],
        multipleHitsOnSplitAce: [false, true],
        canHitAndDoubleOnSplits: [false, true],
    }
}


let sideBets = {

    available: {

        perfectPairMixed: {

            includesSubOptions: true,
            entered: false,
            betAmount: 0,
            dealerInvolved: false,

            mixed: {          

                fulfilled: (hand) => {
                    if(hand[0].cardValue == hand[1].cardValue){
                        return true
                    }
                    return false
                },
                payout: '5:1',
            },
            colored:{                

                fulfilled: (hand) => { 
                    if(hand[0].cardValue == hand[1].cardValue && (hand[0].suitName === 'hearts' || hand[1].suitName === 'diamonds') && (hand[0].suitName === 'hearts' || hand[1].suitName === 'diamonds')){
                        return true
                    }
                    return false
                },
                payout: '12:1'
            },
            perfect:{                

                fulfilled: (hand) => {
                    if(hand[0].cardValue == hand[1].cardValue && hasSameSuit(hand,2)){
                        return true
                    }
                    return false
                },
                payout: '30:1'
            }
        },
        twentyOnePlusThree: {

            includesSubOptions: true,
            entered: false,
            betAmount: 0,

            flush: {                

                fulfilled: (hand) => {
                    if(hasSameSuit(hand,3)) return true
                    return false
                },
                payout: '5:1'
            },
            straight: {                

                fulfilled: (hand) => {
                    if(isStraight(hand,3)) return true
                    return false
                },
                payout: '10:1'
            },
            threeOfAKind: {                

                fulfilled: (hand) => {
                    if(hasSameValue(hand,3)) return true
                    return false
                },
                payout: '30:1'
            },
            straightFlush: {                

                fulfilled: (hand) => {
                    if(isStraight(hand,3) && hasSameSuit(hand,3)) return true
                    return false
                },
                payout: '40:1'
            },
            suitedTrips: {                

                fulfilled: (hand) => {
                    if(hasSameValue(hand,3) && hasSameSuit(hand,3)) return true
                },
                payout: '100:1'
            }

        },
        royalMatch: {

            includesSubOptions: true,
            entered: false,
            betAmount: 0,

            easy: {                

                fulfilled: (hand) => {
                    if(hasSameSuit(hand,2)) return true
                    return false
                },
                payout: '5:2'
            },
            easyBlackjack: {                

                fulfilled: (hand) => {
                    if(hasSameSuit(hand,2) && isBlackjack(hand)) return true
                    return false
                },
                payout: '5:1'
            },
            royal: {                

                fulfilled: (hand) => {
                    if(hasSameSuit(hand,2) && ((hand[0].cardName === 'queen' && hand[1].cardName === 'king') || (hand[0].cardName === 'king' && hand[1].cardName === 'queen'))) return true
                    return false
                },
                payout: '25:1'
            },
        },
        overUnderThirteen: {   

            includesSubOptions: false,
            entered: false,
            betAmount: 0,

            fulfilled: (hand) => {
                let handValueArray = []

                for (let i = 0; i < 2; i++){ //first two cards
                    if(hand[i].cardValue === 'ace') handValueArray.push(1)  //ace counted as one
                    else handValueArray.push(hand[i].cardValue)
                }
                let totalValue = 0
                handValueArray.forEach(value => {
                    totalValue += value
                })
                if(totalValue > 13 || totalValue < 13) return true
                return false
            },
            payout: '1:1'
        },
        superSevens: {

            includesSubOptions: true,
            entered: false,
            betAmount: 0,

            single: {                

                fulfilled: (hand) => {
                    if(hand[0].cardName === 'seven') return true
                    return false
                },
                payout: '3:1'
            },
            pair: {                

                fulfilled: (hand) => {
                    if(hasSameValue(hand,2) && hand[0].cardName === 'seven') return true
                    return false
                },
                payout: '50:1'
            },
            suitedPair: {                

                fulfilled: (hand) => {
                    if(hasSameSuit(hand,2) && hasSameValue(hand,2) && hand[0].cardName === 'seven') return true
                    return false
                },
                payout: '100:1'
            },
            set: {                

                fulfilled: (hand) => {
                    if(hasSameValue(hand,3) && hand[0].cardName === 'seven') return true
                    return false
                },
                payout: '500:1'
            },
            suitedSet: {                

                fulfilled: (hand) => {
                    if(hasSameSuit(hand,3) && hasSameValue(hand,3) && hand[0].cardName === 'seven') return true
                    return false
                },
                payout: '5000:1'
            }
        },
        luckyLadies: {

            includesSubOptions: true,
            entered: false,
            betAmount: 0,

            any: {                

                fulfilled: (hand) => {
                    if(hand[0].cardValue + hand[1].cardValue == 20) return true
                    return false
                },
                payout: '4:1'
            },   
            suited: {                

                fulfilled: (hand) => {
                    if(hasSameSuit(hand,2) && hand[0].cardValue + hand[1].cardValue == 20) return true
                    return false
                },
                payout: '10:1'
            },         
            matching: {                

                fulfilled: (hand) => {
                    if(hand[0].cardName == hand[1].cardName && hasSameSuit(hand,2) && hand[0].cardValue == 10 && hand[1].cardValue == 10) return true
                    return false
                },
                payout: '25:1'
            },
            queensOfHeart: {                

                fulfilled: (hand) => {
                    if(hasSameSuit(hand,2) && hand[0].cardName === 'queen' && hand[1].cardName === 'queen') return true
                    return false
                },
                payout: '200:1'
            },
            queensOfHeartDealerBlackjack: {                

                fulfilled: (hand) => {
                    if(hasSameSuit(hand,2) && hand[0].cardName === 'queen' && hand[1].cardName === 'queen' && isBlackjack(dealerHand)) return true
                    return false
                },
                payout: '1000:1'
            }
        }
        
    }
}

const numberOfDecks = 1
let currentStack = createStackFromDeck(baseDeck,numberOfDecks)
let playerObject = {
    playerName:'RainbowPepe',
    initialBalance: '100',
    playerBet: '10',
    currentBalance: '100',
    hands: {},
    hasInsurance: false
}
////////////////////////////////////////////////////////////////////////////////////////////////

let keepPlaying = true

while (keepPlaying) {

//place bets and sidebets
currentStack = createStackFromDeck(baseDeck,numberOfDecks) //restocks deck

playerObject.playerBet = 10
playerObject.hasInsurance = false //reset insurance-status before new round
playerObject.hands = {}

//start of game
let dealerHand = []

//setup player hand
playerObject = createHandInPlayerObject(playerObject)
playerObject.hands['0'].push(hit(currentStack))
playerObject.hands['0'].push(hit(currentStack))

//console.log("deckafterhit: ", currentStack)
if(isBlackjack(playerObject.hands['0'])) console.log('blackjack!')
else console.log('The value of your hand is: ' ,calculateHandValue(playerObject.hands['0']))

console.log(getFulfilledBets(createSideBetList(sideBets), playerObject.hands['0']))


dealerHand.push(hit(currentStack))
dealerHand.push(hit(currentStack))

console.log(dealerHand[0]) //shows first card of dealer
if(insuranceAvailable(dealerHand)){
    console.log("Dealer has an ace. Do you want to buy insurance?")
    if("<Player buys insurance>"){
        playerObject = setInsurance(playerObject)
    }
}

Object.keys(playerObject.hands).forEach(handIndex => {
    let hand = playerObject.hands[handIndex]
    if(!canDraw(hand)){ 
        console.log('You cannot draw with this hand! Hand-value is ' + calculateHandValue(hand))
        return
    }
    //let interactableEmbed = ""
    //if(isSplitable(hand, splitRules)){ interactableEmbed.add(splitOptionButton) } //pseudo-code 
})

keepGoing = false
console.log('====================')
//console.log('<ALL EARNINGS/GAMESTATS HERE>')
//console.log('Play again?')
}   // end of playing-loop

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
    //console.log(tempReturnObjectUntilFailureFound)
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
        return sumAmount
    }
    else{
        return calculateAce(hand)
    }

}

function calculateAce(hand){

    let aceArray = []
    hand.forEach(card => {
        if(card.cardName === 'ace') aceArray.push(card)
    })
    restArray = hand.filter(card => card.cardName !== 'ace')

    let handWithoutAce = 0
    restArray.forEach(card => {
        handWithoutAce += card.cardValue
    })

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
    return addAceToValue
}

function isSplitable(hand, splitRules) {
    if(hand.length != 2) return false //cant split 1 or 3 cards
    if(hand[0].cardValue != hand[1].cardValue) return false //cant split non-equal cards

    return true
}

function doubleDown(hand){

}

function hit(stack){

    let randomCard = randomDeckCard(stack)
    takeCard(randomCard, stack)
    return randomCard
}

function stand(hand){

}

function createStackFromDeck(baseDeck,numberOfDecks){

    returnStack = baseDeck
    Object.keys(returnStack).forEach(cardName => {
        returnStack[cardName].totalAmount *= numberOfDecks
        //console.log(cardName)
        Object.keys(returnStack[cardName].suits).forEach(suitName => {
            returnStack[cardName].suits[suitName] *= numberOfDecks
        })
    })
    return returnStack
    //console.log(returnStack)
}

function isBlackjack(hand){
    if(hand.length != 2) return false
    if(calculateHandValue(hand) != 21) return false
    return true
}

function splitHand(hand){
    if(!isSplitable(hand, splitRules)) return
    return  [hand[0], hand[1]]
}

function createHandInPlayerObject(playerObject){

    if(!Object.keys(playerObject).includes('hands')){
        console.log('Object doesnt have "hands"-property!')
        return
    }
    const arrayOfHandNames = Object.keys(playerObject.hands)

    if(arrayOfHandNames.length === 0){
        playerObject.hands['0'] = []
        return playerObject
    }
    console.log("getObj:",typeof parseInt(arrayOfHandNames[arrayOfHandNames.length-1]))
    if(isNaN(parseInt(arrayOfHandNames[arrayOfHandNames.length-1]))){
        console.log("Error, couldnt recognize index of object: ",arrayOfHandNames[arrayOfHandNames.length-1])
        return
    }
    let newObjectIndex = parseInt(arrayOfHandNames[arrayOfHandNames.length-1]) + 1
    newObjectIndex = String(newObjectIndex)
    playerObject.hands[newObjectIndex] = []
    return playerObject
}

function setInsurance(playerObject){
    if(!hasNeccessaryKeys(playerObject)) throw new Error("Player-object doesnt have neccessary keys!")
    playerObject.hasInsurance = true
    playerObject.playerBet = playerObject.playerBet * 1.5
    return playerObject
}

function hasNeccessaryKeys(playerObject){
    const requiredKeys = ['currentBalance','initialBalance','hands','playerName','playerBet','hasInsurance']
    const presentKeys = Object.keys(playerObject)
    requiredKeys.forEach(key => {
        if(!presentKeys.includes(key)) return false
    })
    return true
}

function surrender(playerObject){

}


function isBust(hand){
    if(calculateHandValue(hand) <= 21) return false
    return true
}

function insuranceAvailable(dealerHand){
    if(dealerHand[0].cardName === 'ace') return true //if first card of dealer is ace
    return false
}

function canDraw(hand){
    if(isBust(hand)) return false
    if(isBlackjack(hand)) return false
    if(calculateHandValue(hand) >= 21) return false
    //if(!isSplitable(hand, splitRules)) return false
    return true
}

function isStraight(hand,firstXCards){
    let cardValueArray = []
    for(let i = 0; i < firstXCards; i++) {
        cardValueArray.push(hand[i].cardValue)
    }
    /*hand.forEach(card => {
        cardValueArray.push(card.cardValue)
    })*/
    cardValueArray.sort()
    for(let i = 1; i < firstXCards; i++) {
        console.log(cardValueArray[i-1], cardValueArray[i])
        if(cardValueArray[i-1] + 1 != cardValueArray[i]) return false
    }
    return true
}

function hasSameSuit(hand,firstXCards){
    
    for(let i = 1; i < firstXCards; i++) {
        if(hand[i-1].suitName != hand[i].suitName) return false

    }
    return true
}

function hasSameValue(hand,firstXCards){
    for(let i = 1; i < firstXCards; i++) {
        if(hand[i-1].cardValue != hand[i].cardValue) return false
    }
    return true
}

function calculateSideBetEarnings(fulfilledBets){

    let wonBetObject = {}
    let payoutRatio = bet.payout
    payoutRatio = payoutRatio.spilt(':')
    const finalPayout = bet.betAmount * (payoutRatio[0] / payoutRatio[1])

    wonBetObject['betName'] = bet.betName
    wonBetObject['originalBetAmount'] = bet.betAmount
    wonBetObject['payout'] = bet.payout
    wonBetObject['finalPayout'] = finalPayout


}

function filterEqualCategoryBets(fulfilledBets){

    let dublicateCategories = []
    let categoryNamesObject = {}
    fulfilledBets.forEach(bet => {
        if(!Object.keys(categoryNamesObject).includes(bet.betName)){
            categoryNamesObject[bet.betName] = []
        }
        categoryNamesObject[bet.betName].push(bet.betName)
    })

    Object.keys(categoryNamesObject).forEach(categoryName => {
        if(Object.keys(categoryName).length > 1) dublicateCategories.push(categoryName)
    })

    dublicateCategories.forEach(category => {
        categoryNamesObject[category]
    })

}

function getFulfilledBets(sideBets, hand){

    let fulfilledBets = []
    //let sideBetsKeys = Object.keys(sideBets)
    sideBets.forEach(sideBet => {
        if(Object.keys(sideBet).includes('fulfilled')){
            console.log(hand)
            if(sideBet.fulfilled(hand) === true){
                delete sideBet['entered']
                fulfilledBets.push(sideBet)
            }
        }
    })
    return fulfilledBets
}

function createSideBetList(data){
    if(!Object.keys(data)) return false
    let isObjectCandidates = []
    if(!Object.keys(data).includes('available')) return false

    Object.keys(data.available).forEach(key => {
        //console.log(typeof data.available[key])
        if(Object.keys(data.available[key]).includes('includesSubOptions')) {
            if(data.available[key].includesSubOptions === false){

                let appendPropObject = data.available[key]
                delete appendPropObject['includesSubOptions']
                appendPropObject['betName'] = key
                isObjectCandidates.push(appendPropObject)
            }

        }
        if(Object.keys(data.available[key]).includes('includesSubOptions')){

            if(data.available[key].includesSubOptions === true){
                //console.log(Object.keys(data.available[key]))
                Object.keys(data.available[key]).forEach(subOption => {
                    //console.log(subOption)
                    let appendPropObject = {}
                    appendPropObject['betName'] = key
                    appendPropObject['entered'] = data.available[key].entered
                    appendPropObject['betAmount'] = data.available[key].betAmount
                    appendPropObject['fulfilled'] = data.available[key][subOption].fulfilled
                    appendPropObject['payout'] = data.available[key][subOption].payout
                    if(typeof data.available[key][subOption] === 'object') isObjectCandidates.push(appendPropObject)
                })
            }      
        }

    })
    return isObjectCandidates
}