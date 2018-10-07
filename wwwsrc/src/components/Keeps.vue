<template>
    <v-card @mouseenter="showActions(keep.id)" @mouseleave="hideActions(keep.id)">
        <v-img v-if="keep.img.length > 0" :src="keep.img" class="white--text">
            <v-layout column fill-height>
                <v-spacer></v-spacer>
                <v-card-title class="img-title">{{keep.name}}</v-card-title>
            </v-layout>
        </v-img>
        <v-card-title v-if="keep.img.length < 1">{{keep.name}}</v-card-title>
        <v-card-text>{{keep.description}}</v-card-text>
        <v-card-actions :id="keep.id" class="buttons"> 
            <v-btn flat fab small color="amber darken-4">view</v-btn>
            <v-btn flat fab small color="amber darken-4">share</v-btn>
            <v-btn flat fab small color="amber darken-4">keep</v-btn> 
        </v-card-actions>
    </v-card>     
</template>
<script>
export default {
    name: "keeps",
    props: ["keep"],
    methods: {
        showActions(id) {
            $(`#${id}`).css({"display": "flex"})
        },
        hideActions(id) {
            $(`#${id}`).css({"display": "none"})
        }
    }    
}
</script>
<style scoped>
.img-title {
    text-shadow: 0 0 5px black;
    font-size: 120%;
}
.buttons {
    display: none;
}
@media (hover: none) {
    .buttons {
        display: flex;
    }
}
</style>
