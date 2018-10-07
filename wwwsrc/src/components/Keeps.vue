<template>
    <v-content>
        <v-container>
            <v-layout>
                <v-flex sm12 md3 v-for="keep in keeps" :key="keep.id">
                    <v-card pa-5>
                        <v-img v-if="keep.img.length > 0" :src="keep.img" class="white--text">
                            <v-layout column fill-height>
                                <v-spacer></v-spacer>
                                <v-card-title class="img-title">{{keep.name}}</v-card-title>
                            </v-layout>
                        </v-img>
                        <v-card-title v-if="keep.img.length < 1">{{keep.name}}</v-card-title>
                        <v-card-text>{{keep.description}}</v-card-text>
                        <v-card-actions>
                            <v-btn flat color="amber darken-4">view</v-btn>
                            <v-btn flat color="amber darken-4">share</v-btn>
                            <v-btn flat color="amber darken-4">keep</v-btn>
                        </v-card-actions>
                    </v-card>
                </v-flex>
            </v-layout>
        </v-container>
    </v-content>
</template>
<script>
export default {
    name: "keeps",
    computed: {
        keeps() {
            return this.$store.state.keeps
        }
    }    
}
</script>
<style>
.img-title {
    text-shadow: 0 0 5px black;
    font-size: 120%;
}
</style>
