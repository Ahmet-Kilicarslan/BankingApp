'use client';
import {useState, useEffect} from 'react';


export default function Wordle() {

    const rowCount: number = 6;
    const colCount: number = 5;


    type LetterStatus = "correct" | "absent" | "present" | "";

    type Cell = {
        letter: string,
        status: LetterStatus,
    }
    type Board = Cell[][];

    const [gameBoard, setGameBoard] = useState<Board>(
        Array.from({length: rowCount}, () =>
            Array.from({length: colCount}, () => ({
                letter: "",
                status: ""
            }))
        ));

    const [answer, setAnswer] = useState<string>("");

    const [currentRow, setCurrentRow] = useState(0);
    const [currentCol, setCurrentCol] = useState(0);
    const [youWin, setYouWin] = useState(false);
    const [youLooseSucker, setYouLooseSucker] = useState(false);

    interface WordsData {
        words: string[];
    }

    async function fetchWord(): Promise<string> {

        const response = await fetch('/words.json');

        const wordsList: WordsData = await response.json();

        const length = wordsList.words.length;

        const randomIndex: number = Math.floor(Math.random() * length);

        return wordsList.words[randomIndex];


    }

    function getCellColor(letterStatus: LetterStatus): string {

        switch (letterStatus) {
            case "correct":
                return "bg-wordle-correct";
            case "absent":
                return "bg-wordle-absent";
            case "present":
                return "bg-wordle-present";
            default:
                return "bg-surface"
        }

    }

    function handleKeyDown(event: KeyboardEvent) {
        const key: string = event.key.toUpperCase();

     

        if (key === "ENTER") {
            event.preventDefault();
            if (currentCol === colCount) {
                submitGuess();
            }

            return;
        }


        if (key === "BACKSPACE") {
            event.preventDefault();

            if (currentCol > 0) {
                const newBoard = gameBoard.map(row =>
                    row.map(cell => ({...cell}))
                );

                newBoard[currentRow][currentCol - 1].letter = "";
                newBoard[currentRow][currentCol - 1].status = "";

                setGameBoard(newBoard);
                setCurrentCol(currentCol - 1);
            }

            return;
        }

        if (!/^[A-Z]$/.test(key)) {
            return;
        }

        if (currentCol >= colCount) {
            return;
        }

        const newBoard = gameBoard.map(row =>
            row.map(cell => ({...cell}))
        );

        newBoard[currentRow][currentCol].letter = key;


        setCurrentCol(currentCol + 1);

        setGameBoard(newBoard);


    }

   

    function submitGuess() {


        if (!answer) {
            return;
        }

        if (currentCol !== colCount) {
            return;
        }

        const guess = gameBoard[currentRow]
            .map(cell => cell.letter)
            .join("");

        const newBoard = gameBoard.map(row =>
            row.map(cell => ({...cell}))
        );

        let remainingAnswer = answer;

        for (let c = 0; c < colCount; c++) {

            if (guess[c] === answer[c]) {

                newBoard[currentRow][c].status = "correct";

                remainingAnswer = remainingAnswer.replace(
                    guess[c],
                    ""
                );
            }
        }

        for (let c = 0; c < colCount; c++) {

            if (newBoard[currentRow][c].status === "correct") {
                continue;
            }

            if (remainingAnswer.includes(guess[c])) {

                newBoard[currentRow][c].status = "present";

                remainingAnswer = remainingAnswer.replace(
                    guess[c],
                    ""
                );

            } else {

                newBoard[currentRow][c].status = "absent";
            }
        }

        const isCorrect = newBoard[currentRow].every(
            cell => cell.status === "correct"
        );

        if (isCorrect) {
            setYouWin(true);
        } else if (currentRow === rowCount - 1) {
            setYouLooseSucker(true);
        } else {
            setCurrentRow(currentRow + 1);
            setCurrentCol(0);
        }


        setGameBoard(newBoard);


    }


    async function newGame() {
        setGameBoard(
            Array.from({length: rowCount}, () =>
                Array.from({length: colCount}, () => ({
                    letter: "",
                    status: ""
                }))));


        setCurrentRow(0);
        setCurrentCol(0);
        setYouWin(false);
        setYouLooseSucker(false);

        let newAnswer: string = await fetchWord();

        setAnswer(newAnswer.toUpperCase());


    }


    useEffect(() => {
        fetchWord().then(word => {
            setAnswer(word.toUpperCase());
        });

    }, []);


    useEffect(() => {
        window.addEventListener("keydown", handleKeyDown);

        return () => {
            window.removeEventListener("keydown", handleKeyDown);
        };
    }, [gameBoard, currentRow, currentCol, answer, youWin, youLooseSucker]);


    console.log("Answer:", answer);

    return (


        <div className="justify-self-center items-center gap-6 mt-10">

            <div>
                {Array.from({length: rowCount}).map((_, rowIndex) => (
                    <div key={rowIndex} className="flex flex-row gap-2">
                        {Array.from({length: colCount}).map((_, colIndex) => (
                            <div key={`${rowIndex} ${colIndex}`} className={`w-[128px] h-[128px]
                     border border-border rounded ${getCellColor(gameBoard[rowIndex][colIndex].status)}
                     text-white text-2xl font-bold flex 
                     items-center justify-center uppercase mb-2
                      ${youWin && rowIndex === currentRow ? "wordle-win" : ""}
                     `}>

                                {gameBoard[rowIndex][colIndex].letter}
                            </div>
                        ))}

                    </div>
                ))}

            </div>

            <button className="btn-game " onClick={newGame}>Again!</button>


        </div>


    );

}