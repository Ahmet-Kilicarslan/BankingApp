'use client';
import {useState, useEffect} from 'react';

export default function TwentyFortyEight() {


    const count = 4;


    const [currentRow, setCurrentRow] = useState(0);
    const [currentCol, setCurrentCol] = useState(0);

    type Cell = {

        number: number | null;

    }

    type index = {

        x: number;
        y: number;
    }


    type Board = Cell[][];

    type Row = Cell[];

    type Direction = "left" | "right" | "up" | "down";

    const [gameBoard, setGameBoard] = useState<Board>(
        Array.from({length: count}, () =>
            Array.from({length: count}, () => ({
                number: null,
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

    function initialBoard(board: Board): Board {
        return getNewTile(getNewTile(board));
    }

    function getNewTile(board: Board): Board {

        const newBoard: Board = board.map(row => row.map(cell => ({...cell})));

        var indexArray: index[] = [];

        for (let i = 0; i < count; i++) {
            for (let j = 0; j < count; j++) {
                if (newBoard[i][j].number == null) {
                    indexArray.push({x: i, y: j});
                }
            }
        }

        const random = Math.floor(Math.random() * indexArray.length);
        const {x, y} = indexArray[random];

        newBoard[x][y].number = 2;
        newBoard[x][y].isEmpty = false;

        return newBoard;
    }

    function compress(row: Row): Row[] {


        const nonNullCells: Row = row.filter(cell => cell.number !== null);

        const emptySlotsNeeded = row.length - nonNullCells.length

        const nullCels: Row = Array.from({length: emptySlotsNeeded}, () => {
            number: null
        });

        return [...nonNullCells, ...nullCels];

    }

    function transpose(board: Board): Board {

        const newBoard: Board = Array.from({length: count}, (_, row) =>
            Array.from({length: count}, (_, col) => ({
                ...board[col][row],
            }))
        );

        return newBoard;
    }

    function compressRow(row: Row, direction: Direction): Row[] {

        if (direction === "left") {

            return compress(row);

        } else if (direction === "right") {

            const reversed = [...row].reverse();
            const compressed = compress(reversed);
            return compressed.reverse();
        }

    }

    


    function merge(row: Row) {


        let skip:number = 0;

        while (skip < row.length-1) {

            if (row[skip].number == row[skip + 1].number) {
                row[skip + 1].number += row[skip].number;
                row[skip].number = null;
                skip += 2;

            } else skip++;


        }

        return row;


    }
    function moveRow(row: Row, direction: Direction): Row {
        let result = compressRow(row, direction);
        result = merge(result);
        result = compressRow(result, direction);
        return result;
    }

    function moveBoard(board: Board, direction: Direction): Board {

        if (direction === "up" || direction === "down") {
            board = transpose(board);
        }

        let innerDirection: Direction;
        if (direction === "down") {
            innerDirection = "right";
        } else if (direction === "up") {
            innerDirection = "left";
        } else {
            innerDirection = direction;
        }

        let newBoard = board.map(row => moveRow(row, innerDirection));

        if (direction === "up" || direction === "down") {
            newBoard = transpose(newBoard);
        }

        return newBoard;
    }



    function boardsAreEqual(a: Board, b: Board): boolean {
        for (let i = 0; i < rowCount; i++) {
            for (let j = 0; j < colCount; j++) {
                if (a[i][j].number !== b[i][j].number) {
                    return false;
                }
            }
        }
        return true;
    }


    useEffect(() => {
        setGameBoard(prevBoard => {
            const isEmpty = prevBoard.every(row => row.every(cell => cell.number === null));
            return isEmpty ? initialBoard(prevBoard) : prevBoard;
        });
    }, []);
    
    useEffect(() => {

        function handleKeyDown(event: KeyboardEvent) {

            let direction: Direction | null = null;

            switch (event.key) {
                case "ArrowLeft":
                    direction = "left";
                    break;
                case "ArrowRight":
                    direction = "right";
                    break;
                case "ArrowUp":
                    direction = "up";
                    break;
                case "ArrowDown":
                    direction = "down";
                    break;
            }

            if (direction === null) {
                return;
            }

            event.preventDefault();

            const movedBoard = moveBoard(gameBoard, direction);

            if (!boardsAreEqual(gameBoard, movedBoard)) {
                setGameBoard(getNewTile(movedBoard));
            }
        }

        window.addEventListener("keydown", handleKeyDown);

        return () => {
            window.removeEventListener('keydown', handleKeyDown);
        };
    }, [gameBoard]);


    return (

        <div className="flex item-center">

            <div>
                {Array.from({length: count}).map((_, rowIndex) => (
                    <div key={rowIndex} className="flex flex-row gap-2">
                        {Array.from({length: count}).map((_, colIndex) => (
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
    
    

