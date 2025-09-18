import React from "react";
import { useQuery, gql } from "@apollo/client";

const GET_BOOKS = gql`
  query {
    books {
      id
      title
      author
      publishedYear
    }
  }
`;

export default function Books() {
  const { loading, error, data } = useQuery(GET_BOOKS);

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error! {error.message}</p>;
console.log('data->',data);
  return (
    <ul>
      {data.books.map((book) => (
        <li key={book.id}>
          {book.title} by {book.author} ({book.publishedYear})
        </li>
      ))}
    </ul>
  );
}
