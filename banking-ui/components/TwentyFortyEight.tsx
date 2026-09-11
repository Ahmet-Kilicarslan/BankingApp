'use client';
import {useState, useEffect} from 'react';

export default function TwentyFortyEight() {


    const rowCount = 4;
    const colCount = 4;

    const [currentRow, setCurrentRow] = useState(0);
    const [currentCol, setCurrentCol] = useState(0);

    type Cell = {

        number: number | null;
        isEmpty: boolean;

    }

    type index = {

        x: number;
        y: number;
    }


    type Board = Cell[][];


    const [gameBoard, setGameBoard] = useState<Board>(
        Array.from({length: rowCount}, () =>
            Array.from({length: colCount}, () => ({
                number: null,
                isEmpty: true,
            }))
        )
    );

    function getCellColor(number: number | null): string {
        switch (number) {
            case null:
                return "bg-surface";
            case 2:
                return "bg-two";
            case 4:
                return "bg-four";
            case 8:
                return "bg-eight";
            case 16:
                return "bg-sixteen";
            case 32:
                return "bg-thirty-two";
            case 64:
                return "bg-sixty-four";
            case 128:
                return "bg-hundred-twenty-eight";
            case 256:
                return "bg-two-hundred-fifty-six";
            case 512:
                return "bg-five-hundred-twelve";
            case 1024:
                return "bg-ten-twenty-four";
            case 2048:
                return "bg-twenty-forty-eight";
            default:
                return "bg-surface";
        }
    }

    function initialGame(){}

    function getNewTile(board: Board): Board {

        const newBoard: Board = board.map(row => row.map(cell => ({ ...cell })));

        var indexArray: index[] = [];

        for (let i = 0; i < rowCount; i++) {
            for (let j = 0; j < colCount; j++) {
                if (newBoard[i][j].isEmpty) {
                    indexArray.push({ x: i, y: j });
                }
            }
        }

        const random = Math.floor(Math.random() * indexArray.length);
        const { x, y } = indexArray[random];

        newBoard[x][y].number = 2;
        newBoard[x][y].isEmpty = false;

        return newBoard;
    }


    return (

        <div className="flex item-center">

            <div>
                {Array.from({length: rowCount}).map((_, rowIndex) => (
                    <div key={rowIndex} className="flex flex-row gap-2">
                        {Array.from({length: colCount}).map((_, colIndex) => (
                            <div key={`${rowIndex} ${colIndex}`} className={`w-[128px] h-[128px]
                     border border-border rounded ${getCellColor(gameBoard[rowIndex][colIndex].number)}
                     text-white text-5xl font-bold flex 
                     items-center justify-center uppercase mb-2
                     `}>

                                {gameBoard[rowIndex][colIndex].number}
                            </div>
                        ))}

                    </div>
                ))}

            </div>


        </div>


    );

}
    
    

