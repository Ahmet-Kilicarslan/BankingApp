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
            Array.from({length: colCount}, () => "")
        ));

    const [answer, setAnswer] = useState<string>("");

    const [currentRow, setCurrentRow] = useState(0);
    const [currentCol, setCurrentCol] = useState(0);

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

    function handleKeyDown(event: KeyboardEvent) {
        const key: string = event.key.toUpperCase();

        if (key === "ENTER") {
            if (currentCol === colCount) {
                submitGuess();
            }

            return;
        }

        if (!/^[A-Z]$/.test(key)) {
            return;
        }

        if (currentCol >= colCount) {
            return;
        }

        const newBoard = gameBoard.map(row => [...row]);

        newBoard[currentRow][currentCol].letter = key;

        setGameBoard(newBoard);

        setCurrentCol(currentCol + 1);
    }


    function submitGuess() {

        if (currentCol !== colCount) {
            return;
        }

        const guess = gameBoard[currentRow]
            .map(cell => cell.letter)
            .join("");

        const newBoard = gameBoard.map(row => [...row]);

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

        setGameBoard(newBoard);
    }




useEffect(() => {
    window.addEventListener('keydown', handleKeyDown);
    return () => {
        window.removeEventListener('keydown', handleKeyDown);
    }
}, []);


return (
    <div className="justify-self-center mt-10">
        {Array.from({length: rowCount}).map((_, rowIndex) => (
            <div key={rowIndex} className="flex flex-row gap-2">
                {Array.from({length: colCount}).map((_, colIndex) => (
                    <div key={`${rowIndex} ${colIndex}`} className="w-[128px] h-[128px]
                     border-r border-border rounded bg-surface
                     text-white text-2xl font-bold flex 
                     items-center justify-center uppercase mb-2">


                        {gameBoard[rowIndex][colIndex]}
                    </div>
                ))}

            </div>
        ))}

    </div>
);

}